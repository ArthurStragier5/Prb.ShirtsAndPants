using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.ShirtsAndPants.Core
{
    public enum Gender { Male, Female }
    public class Customer
    {
        private string firstname;
        private string lastname;

        private string city;
        private Gender gender;
        private string shirtSize;
        private string pantsSize;
        private string id;

        public string ID
        {
            get { return id; }
        }

        public string Firstname
        {
            get { return firstname; }
            set
            {
                if (value.Trim().Length == 0)
                {
                    throw new Exception("De voornaam mag niet leeg zijn!");
                }
                else
                {
                    firstname = value;

                }
            }
        }
        public string Lastname
        {
            get { return lastname; }
            set
            {
                if (value.Trim().Length == 0)
                {
                    throw new Exception("de naam mag niet leeg zijn!");
                }
                else
                {
                    lastname = value;
                }
            }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        

        public  Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string ShirtSize
        {
            get { return shirtSize; }
            set { shirtSize = value; }
        }

        public string PantsSize
        {
            get { return pantsSize; }
            set { pantsSize = value; }
        }




        public Customer(string firstname, string lastname, string city, Gender gender, string shirtSize, string pantsSize)
        {
            id = Guid.NewGuid().ToString();
            Firstname = firstname;
            Lastname = lastname;
            this.city = city;
            this.gender = gender;
            this.shirtSize = shirtSize;
            this.pantsSize = pantsSize;
        }

        public override string ToString()
        {
            return $"{lastname} {firstname} - ({gender.ToString().Substring(0, 1)})";
        }

        //public void UpdateSizes(string shirtSize, string pantsSize)         Deze staat nu bij Shop
        //{
        //    this.shirtSize = shirtSize;
        //    this.pantsSize = pantsSize;
        //}
    }
}
