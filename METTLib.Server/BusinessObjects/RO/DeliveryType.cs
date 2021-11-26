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
    public class DeliveryType
     : MEBusinessBase<DeliveryType>
    {
        #region " Properties and Methods "

        #region " Properties "

        public static PropertyInfo<int> DeliveryTypeIDProperty = RegisterProperty<int>(c => c.DeliveryTypeID, "ID", 0);
        /// <summary>
        /// Gets the ID value
        /// </summary>
        [Display(AutoGenerateField = false), Key]
        public int DeliveryTypeID
        {
            get { return GetProperty(DeliveryTypeIDProperty); }
        }

        public static PropertyInfo<String> DeliveryTypeProperty = RegisterProperty<String>(c => c.DeliveryTypeName, "Delivery Type", "");
        /// <summary>
        /// Gets and sets the Delivery Type value
        /// </summary>
        [Display(Name = "Delivery Type", Description = ""),
        StringLength(250, ErrorMessage = "Delivery Type cannot be more than 250 characters")]
        public String DeliveryTypeName
        {
            get { return GetProperty(DeliveryTypeProperty); }
            set { SetProperty(DeliveryTypeProperty, value); }
        }

        public static PropertyInfo<Decimal> DeliveryPriceProperty = RegisterProperty<Decimal>(c => c.DeliveryPrice, "Delivery Price", 0D);
        /// <summary>
        /// Gets and sets the Delivery Price value
        /// </summary>
        [Display(Name = "Delivery Price", Description = "")]
        public Decimal DeliveryPrice
        {
            get { return GetProperty(DeliveryPriceProperty); }
            set { SetProperty(DeliveryPriceProperty, value); }
        }

        public static PropertyInfo<Boolean> IsActiveIndProperty = RegisterProperty<Boolean>(c => c.IsActiveInd, "Is Active", false);
        /// <summary>
        /// Gets and sets the Is Active value
        /// </summary>
        [Display(Name = "Is Active", Description = ""),
        Required(ErrorMessage = "Is Active required")]
        public Boolean IsActiveInd
        {
            get { return GetProperty(IsActiveIndProperty); }
            set { SetProperty(IsActiveIndProperty, value); }
        }

        public static PropertyInfo<DateTime?> DeletedDateProperty = RegisterProperty<DateTime?>(c => c.DeletedDate, "Deleted Date");
        /// <summary>
        /// Gets and sets the Deleted Date value
        /// </summary>
        [Display(Name = "Deleted Date", Description = "")]
        public DateTime? DeletedDate
        {
            get
            {
                return GetProperty(DeletedDateProperty);
            }
            set
            {
                SetProperty(DeletedDateProperty, value);
            }
        }

        public static PropertyInfo<int> DeletedByProperty = RegisterProperty<int>(c => c.DeletedBy, "Deleted By", 0);
        /// <summary>
        /// Gets and sets the Deleted By value
        /// </summary>
        [Display(Name = "Deleted By", Description = "")]
        public int DeletedBy
        {
            get { return GetProperty(DeletedByProperty); }
            set { SetProperty(DeletedByProperty, value); }
        }

        public static PropertyInfo<SmartDate> CreatedDateProperty = RegisterProperty<SmartDate>(c => c.CreatedDate, "Created Date", new SmartDate(DateTime.Now));
        /// <summary>
        /// Gets the Created Date value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SmartDate CreatedDate
        {
            get { return GetProperty(CreatedDateProperty); }
        }

        public static PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy, "Created By", 0);
        /// <summary>
        /// Gets the Created By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int CreatedBy
        {
            get { return GetProperty(CreatedByProperty); }
        }

        public static PropertyInfo<SmartDate> ModifiedDateProperty = RegisterProperty<SmartDate>(c => c.ModifiedDate, "Modified Date", new SmartDate(DateTime.Now));
        /// <summary>
        /// Gets the Modified Date value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public SmartDate ModifiedDate
        {
            get { return GetProperty(ModifiedDateProperty); }
        }

        public static PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy, "Modified By", 0);
        /// <summary>
        /// Gets the Modified By value
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int ModifiedBy
        {
            get { return GetProperty(ModifiedByProperty); }
        }

        #endregion

        #region " Methods "

        protected override object GetIdValue()
        {
            return GetProperty(DeliveryTypeIDProperty);
        }

        public override string ToString()
        {
            if (this.DeliveryTypeName.Length == 0)
            {
                if (this.IsNew)
                {
                    return String.Format("New {0}", "Delivery Type");
                }
                else
                {
                    return String.Format("Blank {0}", "Delivery Type");
                }
            }
            else
            {
                return this.DeliveryTypeName;
            }
        }

        #endregion

        #endregion

        #region " Validation Rules "

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        #endregion

        #region " Data Access & Factory Methods "

        protected override void OnCreate()
        {
            // This is called when a new object is created
            // Set any variables here, not in the constructor or NewDeliveryType() method.
        }

        public static DeliveryType NewDeliveryType()
        {
            return DataPortal.CreateChild<DeliveryType>();
        }

        public DeliveryType()
        {
            MarkAsChild();
        }

        internal static DeliveryType GetDeliveryType(SafeDataReader dr)
        {
            var d = new DeliveryType();
            d.Fetch(dr);
            return d;
        }

        protected void Fetch(SafeDataReader sdr)
        {
            using (BypassPropertyChecks)
            {
                int i = 0;
                LoadProperty(DeliveryTypeIDProperty, sdr.GetInt32(i++));
                LoadProperty(DeliveryTypeProperty, sdr.GetString(i++));
                LoadProperty(DeliveryPriceProperty, sdr.GetDecimal(i++));
                LoadProperty(IsActiveIndProperty, sdr.GetBoolean(i++));
                LoadProperty(DeletedDateProperty, sdr.GetValue(i++));
                LoadProperty(DeletedByProperty, sdr.GetInt32(i++));
                LoadProperty(CreatedDateProperty, sdr.GetSmartDate(i++));
                LoadProperty(CreatedByProperty, sdr.GetInt32(i++));
                LoadProperty(ModifiedDateProperty, sdr.GetSmartDate(i++));
                LoadProperty(ModifiedByProperty, sdr.GetInt32(i++));
            }

            MarkAsChild();
            MarkOld();
            BusinessRules.CheckRules();
        }

        protected override Action<SqlCommand> SetupSaveCommand(SqlCommand cm)
        {
            LoadProperty(ModifiedByProperty, Settings.CurrentUser.UserID);

            AddPrimaryKeyParam(cm, DeliveryTypeIDProperty);

            cm.Parameters.AddWithValue("@DeliveryType", GetProperty(DeliveryTypeProperty));
            cm.Parameters.AddWithValue("@DeliveryPrice", GetProperty(DeliveryPriceProperty));
            cm.Parameters.AddWithValue("@IsActiveInd", GetProperty(IsActiveIndProperty));
            cm.Parameters.AddWithValue("@DeletedDate", Singular.Misc.NothingDBNull(DeletedDate));
            cm.Parameters.AddWithValue("@DeletedBy", GetProperty(DeletedByProperty));
            cm.Parameters.AddWithValue("@ModifiedBy", GetProperty(ModifiedByProperty));

            return (scm) =>
            {
    // Post Save
    if (this.IsNew)
                {
                    LoadProperty(DeliveryTypeIDProperty, scm.Parameters["@DeliveryTypeID"].Value);
                }
            };
        }

        protected override void SaveChildren()
        {
            // No Children
        }

        protected override void SetupDeleteCommand(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@DeliveryTypeID", GetProperty(DeliveryTypeIDProperty));
        }

        #endregion

    }

}