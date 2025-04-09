<?php

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

$PASS_ADMIN = "admin";
$PASS_USER = "user";

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

require("api.php");

$pass = PARAM("pass");
$action = PARAM("action");

$role = "";
if ( $pass==$PASS_ADMIN )
	$role = "admin";
else if ( $pass==$PASS_USER )
	$role = "user";
else
	exit;

if ( $action=="purge" )
{
	if ( $role=="admin" )
	{
		PurgeUsers();
		PurgeUsers(90, true);
	}
}

?>

<html>
<body>
<table border="0" width="100%" cellspacing="2" cellpadding="0">

<?php

// User list
/////////////

$users = array();
$userCount = 0;
$activeUserCount = 0;
$recentUserCount = 0;
foreach ( glob("$DIR_USERS*.user") as $filename )
{
	$user = substr($filename, strlen($DIR_USERS), strlen($filename)-strlen($DIR_USERS)-5);
	array_push($users, $user);
	$userCount++;
}

// Game list
/////////////

$dirs = scandir($DIR_AGE.$DIR_GAMES);
$games = array();
$gameCount = 0;
$savegameSizes = array();
$savegameCounts = array();
for ( $i=2 ; $i<count($dirs) ; $i++ )
{
	$game = $dirs[$i];
	array_push($games, $game);
	array_push($savegameSizes, 0);
	array_push($savegameCounts, 0);
	$gameCount++;
}

// Table
/////////

echo "<tr><td></td><td width=\"150\"></td><td width=\"100\"></td><td width=\"100\"></td><td width=\"50\"></td>";
for ( $i=0 ; $i<$gameCount ; $i++ )
	echo "<td align=\"center\" width=\"100\" bgcolor=\"#eeeeee\">" . $games[$i] . "</td>";
echo "</tr>\r\n";

// Users
/////////

$now = time();
for ( $iUser=0, $row=0 ; $iUser<$userCount ; $iUser++ )
{
	$user = GetUser($users[$iUser], "", true, true);
	if ( $user===null || $user->active!=1 )
		continue;
	$row++;
	$activeUserCount += $user->active;
	$isRecent = false;
	$rowcolor = $row%2 ? "#d4f1ff" : "#ffe9d4";

	echo "<tr>";
	echo "<td align=\"center\" bgcolor=\"$rowcolor\">" . $users[$iUser] . "</td>";
	if ( $role=="admin" )
		echo "<td align=\"center\" bgcolor=\"$rowcolor\">" . $user->uid . "</td>";
	else
		echo "<td align=\"center\" bgcolor=\"$rowcolor\">hidden</td>";
	echo "<td align=\"center\" bgcolor=\"$rowcolor\">" . $user->date . "</td>";
	echo "<td align=\"center\" bgcolor=\"$rowcolor\">" . $user->app . "</td>";
	echo "<td align=\"center\" bgcolor=\"$rowcolor\">" . $user->os . "</td>";

	for ( $iGame=0 ; $iGame<$gameCount ; $iGame++ )
	{
		$title = "";
		$color = "#000000";
		if ( $user!=null )
		{
			$color = $rowcolor;
			$filename = $DIR_AGE . $DIR_GAMES . $games[$iGame] . "/$DIR_SAVEGAME" . $user->uid;
			if ( file_exists($filename) )
			{
				$isUsed = true;
				$savegameSizes[$iGame] += filesize($filename);
				$savegameCounts[$iGame]++;
				$days = $now - filemtime($filename);
				$days = round($days/86400);
				if ( $days<30 )
				{
					$isRecent = true;
					$title = "$days days";
					$color = "#00B000";
				}
				else if ( $days<365 )
				{
					$title = round($days/30) . " months";
					$color = "#0000B0";
				}
				else
				{
					$title = round($days/365) . " years";
					$color = "#B00000";
				}
			}
		}
		echo "<td align=\"center\" bgcolor=\"$color\">$title</td>";
	}
	
	echo "</tr>\r\n";

	if ( $isRecent )		
		$recentUserCount++;
}

// Savegame count per game
///////////////////////////

echo "<tr><td></td><td></td><td></td><td></td><td></td>";
for ( $i=0 ; $i<$gameCount ; $i++ )
{
	$count = $savegameCounts[$i];
	echo "<td align=\"center\" bgcolor=\"#eeeeee\">$count</td>";
}
echo "</tr>\r\n";

// Savegame count
//////////////////

$value = 0;
for ( $i=0 ; $i<$gameCount ; $i++ )
	$value += $savegameCounts[$i];
echo "<tr><td></td><td></td><td></td><td></td><td></td><td align=\"center\" bgcolor=\"#eeeeee\" colspan=\"$gameCount\">$value</td></tr>\r\n";

// Savegame size per game
//////////////////////////

echo "<tr><td></td><td></td><td></td><td></td><td></td>";
for ( $i=0 ; $i<$gameCount ; $i++ )
{
	$size = round($savegameSizes[$i] / (1024*1024));
	echo "<td align=\"center\" bgcolor=\"#eeeeee\">$size MB</td>";
}
echo "</tr>\r\n";

// Savegame size
/////////////////

$value = 0;
for ( $i=0 ; $i<$gameCount ; $i++ )
	$value += $savegameSizes[$i];
$value = round($value/(1024*1024));
echo "<tr><td></td><td></td><td></td><td></td><td></td><td align=\"center\" bgcolor=\"#eeeeee\" colspan=\"$gameCount\">$value MB</td></tr>\r\n";

?>

</table>

<br>

<div align="right">
<strong>
User count: <?php echo $userCount ?><br>
Active user count: <?php echo $activeUserCount ?><br>
Recent user count: <?php echo $recentUserCount ?><br>
</strong>
</div>

<br>

<?php
if ( $role=="admin" )
{
	?>
	<div align="center">
	<form action="admin.php" method="post">
	<input type="hidden" name="pass" value="<?php echo GET("pass") ?>">
	<input type="hidden" name="action" value="purge">
	<input type="submit" value="Purge Users">
	</form>
	</div>
	<?php
}
?>

</body>
</html>
