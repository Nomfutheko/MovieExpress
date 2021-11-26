﻿// Generated 16 Nov 2021 11:26 - Singular Systems Object Generator Version 2.2.694
//<auto-generated/>
using System;
using Csla;
using Csla.Serialization;
using Csla.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singular;
using System.Data;
using System.Data.SqlClient;


namespace MELib.Carts
{
    [Serializable]
    public class ShoppingCartList
     : MEBusinessListBase<ShoppingCartList, ShoppingCart>
    {
        #region " Business Methods "

        public ShoppingCart GetItem(int ShoppingCartID)
        {
            foreach (ShoppingCart child in this)
            {
                if (child.ShoppingCartID == ShoppingCartID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Shopping Carts";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public Criteria()
            {
            }

        }

        public static ShoppingCartList NewShoppingCartList()
        {
            return new ShoppingCartList();
        }

        public ShoppingCartList()
        {
            // must have parameter-less constructor
        }

        public static ShoppingCartList GetShoppingCartList()
        {
            return DataPortal.Fetch<ShoppingCartList>(new Criteria());
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(ShoppingCart.GetShoppingCart(sdr));
            }
            this.RaiseListChangedEvents = true;
        }

        protected override void DataPortal_Fetch(Object criteria)
        {
            Criteria crit = (Criteria)criteria;
            using (SqlConnection cn = new SqlConnection(Singular.Settings.ConnectionString))
            {
                cn.Open();
                try
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "GetProcs.getShoppingCartList";
                        using (SafeDataReader sdr = new SafeDataReader(cm.ExecuteReader()))
                        {
                            Fetch(sdr);
                        }
                    }
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        #endregion

    }

}