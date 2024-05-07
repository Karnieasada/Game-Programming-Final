<?php
    $hostname = 'localhost';
    $username = 'root';
    $password = 'root';
    $database = 'hale49';
    $secretKey = "mySecretKey";
     
    try 
    {
        $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
               $username, $password);
    } 
    catch(PDOException $e) 
    {
        echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() 
                ,'</pre>';
    }
     
    $hash = $_GET['hash'];
    $realHash = hash('sha256', $_GET['Name'] . $_GET['Score'] . $secretKey);
        
    if($realHash == $hash) 
    { 
        $sth = $dbh->prepare('INSERT INTO scores VALUES (:Name
                , :Score)');
        try 
        {
            $sth->bindParam(':Name', $_GET['Name'], 
                      PDO::PARAM_STR);
            $sth->bindParam(':Score', $_GET['Score'], 
                      PDO::PARAM_INT);
            $sth->execute();
        }
        catch(Exception $e) 
        {
            echo '<h1>An error has ocurred.</h1><pre>', 
                     $e->getMessage() ,'</pre>';
        }
    }    

?>