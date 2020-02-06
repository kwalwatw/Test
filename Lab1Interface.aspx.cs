//Name: TK Kwalwasser
//Date: 1/20/2020
//Class: CIS 484 Section 2
//Purpose: Interface C# Code for Lab 1
//Pledge: I have followed the JMU Honor Code in the completion of this assignment.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

public partial class Lab1Interface : System.Web.UI.Page
{
    //Global SQL connection
    SqlConnection sc = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sc.ConnectionString = @"Data Source=LOCALHOST;Initial Catalog=Lab1;Integrated Security=True";
        }
        catch (SqlException)
        {
            Console.WriteLine("Error Clearing Database.");
        }
        

    }
    
        
    //When clicking the insert button
    protected void InsertClick(object sender, EventArgs e)
    {
        int companyID = 0;
        String companyname = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sc;
            sc.Open();
            cmd.CommandText = "select CompanyID, TruckingCompany from company";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                    companyID = int.Parse(reader.GetValue(0).ToString());
                    companyname = reader.GetString(1);
                    Company comp = new Company(companyID, companyname);
            }
        }
        catch (Exception)
        {
            FeedBack.Text += "Data Error";
        }
        finally
        {
            sc.Close();
        }


        //Data validation
        //bool user = checkUserName();
        bool text = checkTexts();
        bool name = checkNames();
        //bool phone = checkPhone();
        bool array = checkArray();
        String errorMessage = "";

        //Checks before sending off the info to the array
        //If there is an error when inserting the data
        if (text == false)
        {
            errorMessage += "\nPlease populate all text fields";
        }
        /*if (user == false)
        {
            errorMessage += "Username already exists";
        }*/
        if(name == false)
        {
            errorMessage += "\nName already in the system.";
        }
        //if(phone == false)
        //{
        //    errorMessage += "\nPlease enter a phone number with the valid format. Ex: x-xxx-xxx-xxxx";
        //}
        if(array == false)
        {
            errorMessage += "\nArray is full. Please commit work.";
        }
        if(name == false || text == false|| array == false)
        {
            FeedBack.Text = errorMessage;
        }
        if(text && name && array)
        {//Creating the Driver Object if the validation checks out
            FeedBack.Text = "Driver Successfully Added! Press the Commit Button to add to the DB.";
            Driver driver = new Driver(FirstNameBox.Text, LastNameBox.Text, EmailBox.Text, UserNameBox.Text, PhoneBox.Text, "TK Kwalwasser", DateTime.Now.ToString(), findCompanyID(CompanyList.Text));
            LastUpdatedByBox.Text = "TK Kwalwasser";
            LastUpdatedBox.Text = DateTime.Now.ToString();
            FirstNameBox.Text = "";
            LastNameBox.Text = "";
            EmailBox.Text = "";
            UserNameBox.Text = "";
            PhoneBox.Text = "";
            CompanyList.SelectedIndex = 0;
        }

    }


    //Data validation to make sure the text boxes are all filled out
    protected bool checkTexts()
    {
        bool returnBool = true;
        if(FirstNameBox.Text.Length < 1 || LastNameBox.Text.Length < 1 || EmailBox.Text.Length < 1
           || UserNameBox.Text.Length < 1 || PhoneBox.Text.Length < 1)
        {
            returnBool = false;
        }
        return returnBool;
    }

    //Data validation to make sure the phone number is in the correct format
    protected bool checkPhone()
    {
        bool returnBool = true;
        if (PhoneBox.Text.Equals("Ex: x-xxx-xxx-xxxx") || PhoneBox.Text.Length < 14)
        {
            returnBool = false;
        }
        if(PhoneBox.Text.Contains("-"))
        {
            returnBool = true;
        }
        return returnBool;
    }

    //Data validation to make sure the name doesn't already exist in the array
    protected bool checkNames()
    {
        bool returnBool = true;
        for(int i = 0; i < Driver.arraySize; i++)
        {
            if (Driver.drivers[i] != null)
            {
                if (Driver.drivers[i].getName().ToUpper().Equals(FirstNameBox.Text.ToUpper() + " " + LastNameBox.Text.ToUpper()))
                {
                        returnBool = false;
                }
            }
        }
        return returnBool;
    }

    //Data validation to make sure the username doesn't already exists in the array
    /*protected bool checkUserName()
    {
        bool returnBool = true;
        for (int i = 0; i < Driver.drivers.Length; i++)
        {
            if(Driver.drivers[i] != null)
                if (Driver.drivers[i].getUsername().Equals(UserNameBox.Text))
                {
                    returnBool = false;
                }
        }
        return returnBool;
    }*/

    //Data validation to check if the array is full
    protected bool checkArray()
    {
        bool returnBool = true;
        if(Driver.nextArraySpot >= Driver.arraySize)
        {
            returnBool = false;
        }
        return returnBool;
    }

    //Action event for the populate button
    protected void PopulateClick(object sender, EventArgs e)
    {
        FirstNameBox.Text = "Thomas";
        LastNameBox.Text = "Kwalwasser";
        EmailBox.Text = "tk1@gmail.com";
        UserNameBox.Text = "tkwalwas";
        PhoneBox.Text = "1-203-707-0400";
    }

    //Action event for the Commit button
    protected void CommitClick(object sender, EventArgs e)
    {
        //Finding the max DriverID
        int maxDriverID = 0;
        try
        {
            SqlCommand select = new SqlCommand();
            select.CommandText = "Select max(driverID) FROM Driver;";
            select.Connection = sc;
            sc.Open();
            if (select.ExecuteScalar() != DBNull.Value)
            {
                maxDriverID = Convert.ToInt32(select.ExecuteScalar());
                select.ExecuteNonQuery();
            }
        }
        catch
        {
            Response.Write("");
        }
        //SQL Delete Query to delete all the records except for the one with the highest value of DriverID
        SqlCommand delete = new SqlCommand("DELETE from DRIVER where DriverID NOT IN (Select MAX(DriverID) From Driver);");
        delete.Connection = sc;
        delete.ExecuteNonQuery();
        sc.Close();
        //Try to execute the SQL query to put it into the database
        try
        {
            for (int i = 0; i < Driver.arraySize; i++)
            {
                if (Driver.drivers[i] != null)
                {
                    SqlCommand insert = new SqlCommand("insert into Driver (DriverID, FirstName, LastName, Email, UserName, Phone, CompanyID, LastUpdatedBy, LastUpdated) values (@DriverID, @FirstName, @LastName, @Email, @UserName, @Phone, @CompanyID, @LastUpdatedBy, @LastUpdated)");
                    //SqlCommand insert = new SqlCommand("insert into Driver (DriverID, FirstName, LastName, Email, UserName, Phone, CompanyID, LastUpdatedBy, LastUpdated) values (@DriverID, @FirstName, @LastName, @Email, @UserName, @Phone, @CompanyID, @LastUpdatedBy, @LastUpdated)");
                    sc.ConnectionString = @"Data Source=LOCALHOST;Initial Catalog=Lab1;Integrated Security=True";
                    insert.Parameters.AddWithValue("@DriverID", Driver.drivers[i].getID());
                    insert.Parameters.AddWithValue("@FirstName", Driver.drivers[i].getFirstName());
                    insert.Parameters.AddWithValue("@LastName", Driver.drivers[i].getLastName());
                    insert.Parameters.AddWithValue("@Email", Driver.drivers[i].getEmail());
                    insert.Parameters.AddWithValue("@UserName", Driver.drivers[i].getUsername());
                    insert.Parameters.AddWithValue("@Phone", Driver.drivers[i].getPhone());
                    insert.Parameters.AddWithValue("@CompanyID", Driver.drivers[i].getCompanyId());
                    insert.Parameters.AddWithValue("@LastUpdatedBy", "TK Kwalwasser");
                    insert.Parameters.AddWithValue("@LastUpdated", Driver.drivers[i].getLastUpdated());
                    maxDriverID = Driver.drivers[i].getID();
                    insert.Connection = sc;
                    sc.Open();
                    //Test data to see if the SQL connection operates
                    insert.ExecuteNonQuery();
                    sc.Close();
                }
            }

        }
        catch (SqlException)
        {
            FeedBack.Text = "SQL Error";
        }
        //Resetting the driver array
        for(int i = 0; i < Driver.arraySize; i++)
        {
            Driver.drivers[i] = null;
        }
        Driver.nextArraySpot = 0;
        FeedBack.Text = "Work successfully committed.";
        FirstNameBox.Text = "";
        LastNameBox.Text = "";
        EmailBox.Text = "";
        UserNameBox.Text = "";
        PhoneBox.Text = "";
        CompanyList.SelectedIndex = 0;
        LastUpdatedBox.Text = "";
        LastUpdatedByBox.Text = "";
    }

    //Action event for the clear button
    protected void ClearButtonClick(object sender, EventArgs e)
    {
        FirstNameBox.Text = "";
        LastNameBox.Text = "";
        EmailBox.Text = "";
        UserNameBox.Text = "";
        PhoneBox.Text = "";
        CompanyList.SelectedIndex = 0;
        LastUpdatedBox.Text = "";
        LastUpdatedByBox.Text = "";
    }

    //Action event for the exit button to exit the program
    protected void ExitButtonClick(object sender, EventArgs e)
    {

        Response.Write("<script language="+ "Javascript>");
        Response.Write("window.close()");
        Response.Write("</script>");
    }

    /*protected void SQLDelete()
    {
        SqlCommand delete = new SqlCommand("DELETE from DRIVER where DriverID NOT IN (Select MAX(DriverID) From Driver);");
        delete.Connection = sc;
        sc.Open();
        delete.ExecuteNonQuery();
        sc.Close();
    }*/

    /////////////////////Read in the company IDs//////////////////////////
   protected int findCompanyID(String companyName)
    {
        int companyID = 0;
        for(int i = 0; i < Company.companies.Length; i++)
        {
            if(Company.companies[i] != null)
            {
                if(companyName.Equals(Company.companies[i].getCompanyName()))
                {
                    companyID = Company.companies[i].getCompanyID();
                }
            }
        }
        return companyID;
    }


}