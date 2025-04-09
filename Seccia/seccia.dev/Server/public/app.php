<?php

require("api.php");

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$actions = array("connect", "signup", "reset", "delete");
$site = POST("site");
$clientVersion = intval(POST("version"));
$action = POST("action");
$user = POST("user");
$pass = POST("pass");

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

if ( IsValidEmail($user)==false || strlen($pass)!=10 || IsValid($pass)==false )
{
	echo Fail(1);
	exit;
}

$user = strtolower($user);
$userPath = "./" . GetUserPath($user);

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

if ( $action=="connect" )
{
	$json = GetUser($user, $pass, true);
	if ( $json===null )
	{
		echo Fail(1);
		exit;
	}
	if ( $json->active==0 )
	{
		$json->active = 1;
		$content = @json_encode($json);
		if ( $content===false || @file_put_contents($userPath, $content)===false )
		{
			echo Fail();
			exit;
		}
	}
	echo Success();
}
else if ( $action=="signup" )
{
	$file = @file_get_contents($userPath);
	if ( $file!==false )
	{
		echo Fail(2);
		exit;
	}
	$uid = RandomString(16);
	$pass = RandomString(10);
	$hash = EncryptPass($pass);
	if ( $hash=="" )
	{
		echo Fail();
		exit;
	}
	date_default_timezone_set('UTC');
	$date = date("Y-m-d");
	$app = POST("app");
	$os = POST("os");
	$content = "{ \"uid\": \"$uid\", \"pass\": \"$hash\", \"active\": 0, \"date\": \"$date\", \"app\": \"$app\", \"os\": \"$os\", \"token\": \"\" }";
	if ( @file_put_contents($userPath, $content)===false )
	{
		echo Fail();
		exit;
	}
	$msg = "Hi,\r\n";
	$msg .= "Your login is $user\r\n";
	$msg .= "Your pass is $pass\r\n";
	@mail($user, "Account CREATED", $msg, GetMailHeader());
	echo Success();
}
else if ( $action=="reset" )
{
	$json = GetUser($user, "", true, true);
	if ( $json===null )
	{
		echo Fail(1);
		exit;
	}
	$json->token = RandomString(24);
	$content = @json_encode($json);
	if ( $content===false )
	{
		echo Fail();
		exit;
	}
	if ( @file_put_contents($userPath, $content)===false )
	{
		echo Fail();
		exit;
	}
	$msg = "Hi,\r\n";
	$msg .= "Reset your password here:\r\n";
	$msg .= $site . "reset.php?email=" . $user . "&token=" . $json->token . "\r\n";
	@mail($user, "Account PASSWORD", $msg, GetMailHeader());
	echo Success();
}
else if ( $action=="delete" )
{
	$json = GetUser($user, $pass, true);
	if ( $json===null )
	{
		echo Fail(1);
		exit;
	}
	RemoveUser($user);
	$msg = "Hi,\r\n";
	$msg .= "Your account $user was deleted.\r\n";
	@mail($user, "Account DELETED", $msg, GetMailHeader());
	echo Success();
}
else
{
	echo Fail();
}

?>