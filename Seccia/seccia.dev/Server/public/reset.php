<?php

require("api.php");

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$email = GET("email");
$token = GET("token");

if ( IsValidEmail($email)==false || IsValid($token)==false )
{
	echo "Error";
	exit;
}

$json = GetUser($email, "", true, true);
if ( $json===null )
{
	echo "Error";
	exit;
}

if ( $json->token!=$token )
{
	echo "Error";
	exit;
}

$pass = RandomString(10);
$hash = EncryptPass($pass);
if ( $hash=="" )
{
	echo "Error";
	exit;
}

$json->token = "";
$json->pass = $hash;
$content = @json_encode($json);
if ( $content!==false )
	@file_put_contents("./".GetUserPath($email), $content);

$msg = "Hi,\r\n";
$msg .= "Your login is $email\r\n";
$msg .= "Your pass is $pass\r\n";
@mail($email, "Seccia Space Account PASSWORD", $msg, GetMailHeader());

echo "Password changed.<br>Check your mailbox."

?>