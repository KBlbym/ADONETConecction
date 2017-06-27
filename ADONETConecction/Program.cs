
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETConecction
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            //Conectando a la base de datos
            SqlConnection dbCon = new SqlConnection(
                "Server=tcp:khalifaserver.database.windows.net,1433;" +
                "Initial Catalog=TestDataBase;" +
                "Persist Security Info=False;" +
                "User ID=khalifa;" +
                "Password=ClaseWebApp17;MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;");

             dbCon.Open();

            using (dbCon)

            {
                // Mostrando Nº de productos
                SqlCommand Countcommand = new SqlCommand("SELECT COUNT(*) FROM SalesLT.Product", 
                                        dbCon);
                int productsCount = (int)Countcommand.ExecuteScalar();
                Console.WriteLine("Products count: {0} ", productsCount);
                Console.ReadKey();

                //Mostrando los nombres de los productos
                SqlCommand NameCoummand = new SqlCommand("SELECT Name FROM SalesLT.Product", dbCon);
                SqlDataReader reader = NameCoummand.ExecuteReader();
                int i = 1, j = 1;
                while (reader.Read())
                    {
                        string Name = (string)reader["Name"];
                        Console.WriteLine("{0} Nombre: {1}",i, Name );
                    i++;
                    }
                reader.Close();
                Console.ReadKey();

                //Mostrando Nombre y pesos de los productos con peso mayor que 1000
                SqlCommand Weightcommand = new SqlCommand("SELECT Name, Weight FROM  SalesLT.Product WHERE Weight > 1000 ", dbCon);
                reader = Weightcommand.ExecuteReader();
                while (reader.Read())
                {
                    string Name = (string)reader["Name"];
                    decimal Weight = (decimal)reader["Weight"];
                    Console.WriteLine("{2} , Nombre: {0} \t Peso: {1}", Name, Weight, j);
                    j++;
                }
                reader.Close();
                Console.ReadKey();

                // mostrando prodoctos por color
                var color = "Red";
                SqlCommand ColorCommand = new SqlCommand(
                    "SELECT Name, Color From  SalesLT.Product WHERE Color = @color", dbCon);
                ColorCommand.Parameters.AddWithValue("@color", color);
                reader = ColorCommand.ExecuteReader();
                while (reader.Read())
                {
                    string Name = (string)reader["Name"];
                    string Color = (string)reader["Color"];
                    Console.WriteLine("Nombre: {0} \t Peso: {1}", Name, Color);
                    
                }

                reader.Close();
                Console.WriteLine("toca cualquier tecla para hacer registro en tabla model");
                Console.ReadKey();

                InsertProductModel("Nuevo2", Guid.NewGuid(), DateTime.Now, dbCon);
                Console.ReadKey();

                Console.WriteLine("toca cualquier tecla para hacer registro en tabla category");
                Console.ReadKey();

                InsertProductcategory("Nuevocatg", Guid.NewGuid(), DateTime.Now, dbCon);
                Console.ReadKey();


            }
            
        }
        //insertar datos en tabla products
        //primero insertamos datos en productModel
        private static void InsertProductModel(string name, Guid rowguid, DateTime ModifiedDate, SqlConnection dbCon)

        {

            SqlCommand cmdInsertDataProductoModel = new SqlCommand(
                "INSERT INTO SalesLT.ProductModel (Name, rowguid, ModifiedDate) " +
                "VALUES (@name, @guidid, @date)", dbCon);
            cmdInsertDataProductoModel.Parameters.AddWithValue("@name", name);
            cmdInsertDataProductoModel.Parameters.AddWithValue("@guidid", rowguid);
            cmdInsertDataProductoModel.Parameters.AddWithValue("@date", ModifiedDate);
            var x = cmdInsertDataProductoModel.ExecuteNonQuery();
            if (x == 1)
            {
                Console.WriteLine("Exitoso");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Error");
                Console.ReadKey();

            }
            SqlCommand CommandgetId = new SqlCommand("SELECT ProductModelID  From  SalesLT.ProductModel WHERE rowguid = @guidid", dbCon);
            CommandgetId.Parameters.AddWithValue("@guidid", rowguid);
            int getId = (int)CommandgetId.ExecuteScalar();


        }
        //insertar datos en product category
        private static void InsertProductcategory(string name, Guid guidid, DateTime ModifiedDate, SqlConnection dbCon)

        {

            SqlCommand cmdInsertDataProductcategory = new SqlCommand("INSERT INTO SalesLT.ProductCategory (Name, rowguid, ModifiedDate) VALUES (@name, @guidid, @date)", dbCon);
            cmdInsertDataProductcategory.Parameters.AddWithValue("@name", name = "Categoria");
            cmdInsertDataProductcategory.Parameters.AddWithValue("@guidid", guidid = Guid.NewGuid());
            cmdInsertDataProductcategory.Parameters.AddWithValue("@date", ModifiedDate = DateTime.Now);
            var result = cmdInsertDataProductcategory.ExecuteNonQuery();
            if (result == 1)
            {
                Console.WriteLine("Exitoso");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Error");
                Console.ReadKey();

            }

            SqlCommand CommandgetId = new SqlCommand("SELECT ProductCategoryID  From  SalesLT.ProductCategory WHERE rowguid = @guidid", dbCon);
            var getId = CommandgetId.Parameters.AddWithValue("@guidid", guidid);

        }
        // insertar dato en product tabla
        private void InsertProducts(string name, int poductsCategoryId, int productModelId,  string productNumber, string color, decimal standardCost, decimal listPrice, DateTime sellstardata,  Guid rowguid, DateTime  ModifiedDate, SqlConnection dbCon)

        {

            SqlCommand cmdInsertDataProduct = new SqlCommand("INSERT INTO SalesLT.Product (Name, ProductNumber, Color,ProductCategoryID, ProductModelID,  StandardCost, ListPrice,SellStartDate, rowguid,   ModifiedDate) VALUES (@name, @productNumber, @color, @poductsCategoryId, @productModelId, @standardCost, @listPrice, @sellstardata,  @rowguid, @ModifiedDate)", dbCon);
            cmdInsertDataProduct.Parameters.AddWithValue("@name", name);
            cmdInsertDataProduct.Parameters.AddWithValue("@productNumber", productNumber);
            cmdInsertDataProduct.Parameters.AddWithValue("@color", color);
            cmdInsertDataProduct.Parameters.AddWithValue("@standardCost", standardCost);
            cmdInsertDataProduct.Parameters.AddWithValue("@listPrice", listPrice);
            cmdInsertDataProduct.Parameters.AddWithValue("@poductsCategoryId", poductsCategoryId  );
            cmdInsertDataProduct.Parameters.AddWithValue("@productModelId", productModelId);
            cmdInsertDataProduct.Parameters.AddWithValue("@sellstardata", sellstardata);
            cmdInsertDataProduct.Parameters.AddWithValue("@rowguid", rowguid);
            cmdInsertDataProduct.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
            cmdInsertDataProduct.ExecuteNonQuery();

        }

        //private void InsertProject(string color)
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Name, Color From  SalesLT.Product @color", dbCon);

        //    cmd.Parameters["@name"].Value = color);

        //    cmd.ExecuteNonQuery();

        //}
    }
}
