<?php
    $hostname = 'localhost';
    $username = 'root';
    $password = 'root';
    $database = 'hale49';
 
    try 
    {
	    $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
             $username, $password);
    } 
    catch(PDOException $e) 
    {
	    echo '<h1>An error has occurred.</h1><pre>', $e->getMessage()
                ,'</pre>';
    }
 
    $sth = $dbh->query('SELECT * FROM scores ORDER BY Score DESC LIMIT 10');
    $sth->setFetchMode(PDO::FETCH_ASSOC);
 
    $result = $sth->fetchAll();
 
    if (count($result) > 0) 
    {
	    foreach($result as $r) 
	    {   
		    echo $r['Name'], "\n _";
		    echo $r['Score'], "\n _";
	    }
    }
?>