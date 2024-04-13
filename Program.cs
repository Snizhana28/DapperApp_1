using Dapper;
using DapperApp_1;
using System.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    connection.Execute("create table Product(id int primary key identity, name varchar(64), price int)");

    //create products
    CreateProduct(connection, new Product { Name = "Product 1", Price = 100 });
    CreateProduct(connection, new Product { Name = "Product 2", Price = 110 });

    //Get all products
    List<Product> products = GetProducts(connection);
    Console.WriteLine("All Products:");
    foreach (var product in products)
    {
        Console.WriteLine($"{product.Id}: {product.Name} - ${product.Price}");
    }

    //Update product
    UpdateProduct(connection, new Product { Id = 1, Name = "Product 1 Updated", Price = 150 });

    //Delete product
    DeleteProduct(connection, new Product { Id = 2 });

}
static void CreateProduct(SqlConnection connection, Product product)
{
    connection.Execute("INSERT INTO Product (Name, Price) VALUES (@Name, @Price)", product);
}
static List<Product> GetProducts(SqlConnection connection)
{
    return connection.Query<Product>("SELECT * FROM Product").AsList();
}

static void UpdateProduct(SqlConnection connection, Product product)
{
    connection.Execute("UPDATE Product SET Name = @Name, Price = @Price WHERE Id = @Id", product);
}

static void DeleteProduct(SqlConnection connection, Product product)
{
    connection.Execute("DELETE FROM Product WHERE Id = @Id", product);
}

