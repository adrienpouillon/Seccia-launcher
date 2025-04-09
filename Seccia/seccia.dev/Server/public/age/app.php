<?php

require("../api.php");

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$actions = array("download", "upload");
$clientVersion = intval(POST("version"));
$action = POST("action");
$user = POST("user");
$pass = POST("pass");
$game = POST("game");
$data = POST("data");

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

if ( $clientVersion!=$CLIENT_VERSION )
{
	echo Fail();
	exit;
}

if ( strlen($action)>32 || in_array($action, $actions)==false )
{
	echo Fail();
	exit;
}

if ( strlen($pass)!=10 || strlen($game)>32 )
{
	echo Fail();
	exit;
}

if ( IsValidEmail($user)==false || IsValid($pass)==false || IsValid($game)==false )
{
	echo Fail();
	exit;
}

$json = GetUser($user, $pass);
if ( $json===null )
{
	echo Fail();
	exit;
}

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

if ( $action=="download" )
{
	$path = $DIR_GAMES . $game . "/" . $DIR_SAVEGAME . $json->uid;
	echo Success($path);
}
else if ( $action=="upload" )
{
	if ( strlen($data)>100000 )
	{
		echo Fail();
		exit;
	}
	$zip = @base64_decode($data);
	if ( $zip===null )
	{
		echo Fail();
		exit;
	}
	$path = "./" . $DIR_GAMES . $game . "/" . $DIR_SAVEGAME . $json->uid;
	if ( @file_put_contents($path, $zip)===false )
	{
		echo Fail();
		exit;
	}
	echo Success();
}
else
{
	echo Fail();
}

?>