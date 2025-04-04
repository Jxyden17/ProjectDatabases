using Microsoft.Data.SqlClient;
using ProjectDatabases.Models;
using System.Diagnostics;

namespace ProjectDatabases.Repositories
{
    public class OrderRepository : ConnectionDatabase ,IOrderRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void AddOrder(Order order)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                // Start a SQL transaction to ensure both operations succeed together
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Check current stock
                        string checkStockQuery = @"SELECT stock FROM DRINK WHERE drink_id = @DrinkId;";
                        using (SqlCommand checkStockCommand = new(checkStockQuery, connection, transaction))
                        {
                            checkStockCommand.Parameters.AddWithValue("@DrinkId", order.SelectedDrinkId);
                            int currentStock = (int?)checkStockCommand.ExecuteScalar() ?? 0;

                            if (currentStock < order.DrinkAmount)
                            {
                                throw new Exception("Not enough stock available.");
                            }
                        }

                        // Step 2: Insert the order
                        string insertOrderQuery = @"INSERT INTO [ORDER] (student_number, drink_id, amount) VALUES (@StudentNumber, @DrinkId, @Amount);";

                        using (SqlCommand insertOrderCommand = new(insertOrderQuery, connection, transaction))
                        {
                            insertOrderCommand.Parameters.AddWithValue("@StudentNumber", order.SelectedStudentId);
                            insertOrderCommand.Parameters.AddWithValue("@DrinkId", order.SelectedDrinkId);
                            insertOrderCommand.Parameters.AddWithValue("@Amount", order.DrinkAmount);
                            insertOrderCommand.ExecuteNonQuery();
                        }

                        // Step 3: Decrease stock only if enough is available
                        string updateStockQuery = @"UPDATE DRINK SET stock = stock - @Amount WHERE drink_id = @DrinkId;";

                        using (SqlCommand updateStockCommand = new(updateStockQuery, connection, transaction))
                        {
                            updateStockCommand.Parameters.AddWithValue("@DrinkId", order.SelectedDrinkId);
                            updateStockCommand.Parameters.AddWithValue("@Amount", order.DrinkAmount);
                            updateStockCommand.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Commit the transaction if all queries succeed
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Undo all changes if any query fails
                        throw new Exception("Transaction failed: " + ex.Message);
                    }
                }
            }
        }
    }
}
