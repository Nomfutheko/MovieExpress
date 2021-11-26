﻿// Generated 12 Nov 2021 11:21 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.RO
{
    [Serializable]
    public class DeliveryTypeList
     : MEBusinessListBase<DeliveryTypeList, DeliveryType>
    {
        #region " Business Methods "

        public DeliveryType GetItem(int DeliveryTypeID)
        {
            foreach (DeliveryType child in this)
            {
                if (child.DeliveryTypeID == DeliveryTypeID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Delivery Types";
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

        public static DeliveryTypeList NewDeliveryTypeList()
        {
            return new DeliveryTypeList();
        }

        public DeliveryTypeList()
        {
            // must have parameter-less constructor
        }

        public static DeliveryTypeList GetDeliveryTypeList()
        {
            return DataPortal.Fetch<DeliveryTypeList>(new Criteria());
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(DeliveryType.GetDeliveryType(sdr));
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
                        cm.CommandText = "GetProcs.getDeliveryTypeList";
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