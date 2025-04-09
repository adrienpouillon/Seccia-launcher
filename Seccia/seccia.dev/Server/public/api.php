<?php

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$EMAIL = "Your Name <noreply@domain.com>";

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$SERVER_VERSION = 1;
$CLIENT_VERSION = 1;

$DIR_USERS = "users/";
$DIR_AGE = "age/";
$DIR_GAMES = "games/";
$DIR_SAVEGAME = "savegame/";

function GET($name)
{
	if ( isset($_GET[$name])==false )
		return "";
	return $_GET[$name];
}

function POST($name)
{
	if ( isset($_POST[$name])==false )
		return "";
	return $_POST[$name];
}

function PARAM($name)
{
	$value = POST($name);
	if ( $value=="" )
		$value = GET($name);
	return $value;
}

function GetMailHeader()
{
	global $EMAIL;
	return "From: $EMAIL";
}

function RandomString($len)
{
	$data = "";
	if ( function_exists("random_bytes") )
		$data = random_bytes(ceil($len/2));
	elseif ( function_exists("openssl_random_pseudo_bytes") )
		$data = openssl_random_pseudo_bytes(ceil($len/2));
	else
		return "";
	return substr(bin2hex($data), 0, $len);
}

function EncryptPass($pass)
{
	if ( $pass=="" )
		return "";
	$hash = @password_hash(strtolower($pass), PASSWORD_DEFAULT);
	if ( $hash===null || $hash===false )
		return "";
	return $hash;
}

function VerifyPass($pass, $hash)
{
	if ( $pass=="" || $hash=="" )
		return false;
	return @password_verify(strtolower($pass), $hash);
}

function IsValid($text)
{
	if ( $text=="" )
		return false;
	return ctype_alnum($text);
}

function IsValidEmail($text)
{
	if ( $text=="" )
		return false;
	return filter_var($text, FILTER_VALIDATE_EMAIL);
}

function GetUserPath($email)
{
	global $DIR_USERS;
	if ( IsValidEmail($email)==false )
		return "";
	$email = strtolower($email);
	return $DIR_USERS . $email . ".user";
}

function GetUser($user, $pass, $root = false, $noPass = false)
{
	$path = "./";
	if ( $root==false )
		$path = "../";
	$path .= GetUserPath($user);
	$file = @file_get_contents($path);
	if ( $file===false )
		return null;
	$json = @json_decode($file);
	if ( $json===null || strlen($json->uid)!=16 )
		return null;
	if ( $noPass==false && VerifyPass($pass, $json->pass)==false )
		return null;
	return $json;
}

function RemoveUser($email)
{
	global $DIR_USERS, $DIR_AGE, $DIR_GAMES, $DIR_SAVEGAME;
	
	$dirs = scandir($DIR_AGE.$DIR_GAMES);
	$games = array();
	$gameCount = 0;
	for ( $i=2 ; $i<count($dirs) ; $i++ )
	{
		array_push($games, $dirs[$i]);
		$gameCount++;
	}

	$user = GetUser($email, "", true, true);
	if ( $user==null )
		return;
		
	$folder = "./$DIR_AGE$DIR_GAMES";
	for ( $i=0 ; $i<$gameCount ; $i++ )
	{
		$path = $folder . $games[$i] . "/$DIR_SAVEGAME" . $user->uid;
		if ( file_exists($path) )
			@unlink($path);
	}
	
	$path = GetUserPath($email);
	if ( file_exists($path) )
		@unlink($path);
}

function PurgeUsers($maxDays = 10, $purgeActiveUsersWithoutGames = false)
{
	global $DIR_USERS, $DIR_AGE, $DIR_GAMES, $DIR_SAVEGAME;
	
	$games = array();
	$gameCount = 0;
	if ( $purgeActiveUsersWithoutGames )
	{
		$dirs = scandir($DIR_AGE.$DIR_GAMES);
		for ( $i=2 ; $i<count($dirs) ; $i++ )
		{
			$game = $dirs[$i];
			array_push($games, $game);
			$gameCount++;
		}
	}

	$users = array();
	$userCount = 0;
	foreach ( glob("$DIR_USERS*.user") as $filename )
	{
		$user = substr($filename, strlen($DIR_USERS), strlen($filename)-strlen($DIR_USERS)-5);
		array_push($users, $user);
		$userCount++;
	}
	
	$now = time();
	date_default_timezone_set('UTC');
	for ( $i=0 ; $i<$userCount ; $i++ )
	{
		$user = GetUser($users[$i], "", true, true);
		if ( $user==null )
			continue;
		if ( $user->active==1 )
		{
			if ( $purgeActiveUsersWithoutGames==false )
				continue;
			$hasGame = false;
			for ( $iGame=0 ; $iGame<$gameCount ; $iGame++ )
			{
				$filename = $DIR_AGE . $DIR_GAMES . $games[$iGame] . "/$DIR_SAVEGAME" . $user->uid;
				if ( file_exists($filename) )
				{
					$hasGame = true;
					break;
				}
			}
			if ( $hasGame )
				continue;
		}
		if ( $maxDays==0 )
			RemoveUser($users[$i]);
		else
		{
			$days = $now - strtotime($user->date);
			$days = round($days/86400);
			if ( $days>$maxDays )
				RemoveUser($users[$i]);
		}
	}
}

function Fail($error = -1)
{
	return "{ \"error\": $error }";
}

function Success($param = "")
{
	global $SERVER_VERSION;
	return "{ \"error\": 0, \"version\": $SERVER_VERSION, \"param\": \"$param\" }";
}

?>