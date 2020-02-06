//Author: TK Kwalwasser
//Date: 1/16/2020
//Class CIS 484 Section 2
//Purpose: Lab 1 CDF for Driver
//Pledge: I have followed the JMU Honor Code in the completion of this assignment.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Driver
{
    //Non static data fields
    private int driverID;
    private String firstName;
    private String lastName;
    private String fullName;
    private String email;
    private String username;
    private String phoneNumber;
    private Company company;
    private int companyID;
    private String lastUpdatedBy;
    private String lastUpdated;
    private int CompanyID;
    public const int arraySize = 3;

    //Static data fields
    public static int nextDriverID = 0;
    public static int nextArraySpot = 0;
    public static Driver[] drivers = new Driver[arraySize];

    //Driver constructor
    public Driver(String first, String last, String email, String username, String phone, String lastUpdatedBy, String lastUpdated, int companyID)
    {
        setName(first, last);
        setFirstName(first);
        setLastName(last);
        setEmail(email);
        setUsername(username);
        setPhone(phone);
        setDriverID();
        setLastUpdatedBy(lastUpdatedBy);
        setLastUpdated(lastUpdated);
        setCompanyID(companyID);
        drivers[nextArraySpot++] = this;
    }

    //Setter for Full name
    public void setName(String first, String last)
    {
        this.fullName = first + " " + last;
    }

    public void setFirstName(String first)
    {
        this.firstName = first;
    }

    public void setLastName(String last)
    {
        this.lastName = last;
    }
    //Setter for email
    public void setEmail(String email)
    {
        this.email = email;
    }

    //Setter for username
    public void setUsername(String username)
    {
        this.username = username;
    }

    //Setter for phone number
    public void setPhone(String phone)
    {
        this.phoneNumber = phone;
    }

    //Setter for DriverID
    public void setDriverID()
    {
        this.driverID = nextDriverID++;
    }

    //Getter for Driver name
    public String getName()
    {
        return this.firstName + " " + this.lastName;
    }

    //Getter for username
    public String getUsername()
    {
        return this.username;
    }

    //Getter for DriverID
    public int getID()
    {
        return this.driverID;
    }

    //Getter for first name
    public String getFirstName()
    {
        return this.firstName;
    }

    //Getter for last name
    public String getLastName()
    {
        return this.lastName;
    }

    //Getter for email
    public String getEmail()
    {
        return this.email;
    }

    //Getter for phone
    public String getPhone()
    {
        return this.phoneNumber;
    }

    //Getter for companyID for the specific driver
    public int getCompanyId()
    {
        return this.companyID;
    }

    //Setter for company for the specific driver
    public void setCompanyID(int company)
    {
        this.companyID = company;
    }
    //Setter for Last updated by
    public void setLastUpdatedBy(String lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }
    //Getter for last updated by
    public String getLastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }

    //Setter for last updated
    public void setLastUpdated(String lastUpdated)
    {
        this.lastUpdated = lastUpdated;
    }

    //Getter for last updated
    public String getLastUpdated()
    {
        return this.lastUpdated;
    }




}
