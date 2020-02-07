//Author: TK Kwalwasser
//Date: 1/16/2020
//Class: CIS 484 Section 2
//Purpose: Lab 1 Company CDF
//Pledge: I have followed the JMU Honor Code in the completion of this assignment.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Company
{

    //Non static data fields
    private int companyID;
    private String truckingCompany;
    private String houseNumber;
    private String streetName;
    private String cityCounty;
    private String homeState;
    private String country;
    private String zip;
    private String address;
    private String trey;

    //Static Data fields
    public static int nextCompanyID;
    public static int nextCompanySpot;
    public static Company[] companies = new Company[100];

    //Constructor for US Company
    public Company(int companyID, String company, String houseNumber, String streetName, String county, String state, String country, String zip)
    {
        this.setCompany(company);
        this.setAddressUS(houseNumber, streetName, county, state, country, zip);
        this.setCompanyID(companyID);
        companies[nextCompanySpot++] = this;
    }

    //Constructor for International Company
    public Company(int companyID, String company, String houseNumber, String streetName, String county, String country, String zip)
    {
        this.setCompany(company);
        this.setAddressNonUS(houseNumber, streetName, county, country, zip);
        this.setCompanyID(companyID);
        companies[nextCompanySpot++] = this;
    }

    public Company(int companyID, String name)
    {
        this.setCompanyID(companyID);
        this.setCompany(name);
        companies[nextCompanySpot++] = this;
    }

    //Setter for company name
    public void setCompany(String company)
    {
        this.truckingCompany = company;
    }

    //Setter for Address for US companies
    public void setAddressUS(String houseNumber, String streetName, String county, String state, String country, String zip)
    {
        this.address = houseNumber + " " + streetName + " " + county + ", " + country + " " + zip;
    }

    //Setter for address for International companies
    public void setAddressNonUS(String houseNumber, String streetName, String county, String country, String zip)
    {
        this.address = houseNumber + " " + streetName + " " + county + ", " + country + " " + zip;
    }
    
    //Setter for companyID
    public void setCompanyID(int companyID)
    {
        this.companyID = companyID;
    }

    //Getter for companyID
    public int getCompanyID()
    {
        return this.companyID;
    }

    public String getCompanyName()
    {
        return this.truckingCompany;
    }
}
