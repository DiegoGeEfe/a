<!DOCTYPE html>
<html>
 
<head>
    <title>Insert Page page</title>
</head>
 
<body>
    <center>
        <?php
 
        // servername => localhost
        // username => root
        // password => empty
        // database name => prototipodb
        $conn = mysqli_connect("localhost", "root", "", "prototipodb");
         
        // Check connection
        if($conn === false){
            die("ERROR: Could not connect. "
                . mysqli_connect_error());
        }
         
        // Taking all 6 values from the form data(input)
        $sesion =  $_REQUEST['sesion'];
        $viglesia = $_REQUEST['viglesia'];
        $vcubo =  $_REQUEST['vcubo'];
        $ccubo = $_REQUEST['ccubo'];
        $ciglesia = $_REQUEST['ciglesia'];


        // Insert in existing row if the name matches, or new row if it doesn't       
         $query = "SELECT * FROM sesiones WHERE sesion = '$sesion'";
         $result= mysqli_query($conn, $query);
         $n = mysqli_num_rows($result);
         if ($n > 0) 
         {
             echo "Found 1 record";
     
             $query = "UPDATE sesiones SET viglesia ='$viglesia', vcubo = $vcubo,
             ccubo = $ccubo, ciglesia = $ciglesia WHERE sesion = '$sesion'";
             
         } 
         
         else
         {
     
             $sql = "INSERT INTO sesiones  VALUES ('$sesion',
            '$viglesia','$vcubo','$ccubo','$ciglesia')";
     
         } 


         $result= mysqli_query($conn, $query);
       
       
       
         if(mysqli_query($conn, $sql)){
            echo "<h3>data stored in a database successfully."
                . " Please browse your localhost php my admin"
                . " to view the updated data</h3>";
 
        } else{
            echo "ERROR: Hush! Sorry $sql. "
                . mysqli_error($conn);
        }
         
        ?>
    </center>
</body>
 
</html>