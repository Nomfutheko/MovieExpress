﻿// Generated 03 Nov 2021 09:37 - Singular Systems Object Generator Version 2.2.694
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
    public class ProductCategories
     :MEBusinessBase<ProductCategories>
    {
        #region " Properties and Methods "

        #region " Properties "

        public static PropertyInfo<int> ProductCategoriessIDProperty = RegisterProperty<int>(c => c.ProductCategoriessID, "ID", 0);
        /// <summary>
        /// Gets the ID value
        /// </summary>
        [Display(AutoGenerateField = false), Key]
        public int ProductCategoriessID
        {
            get { return GetProperty(ProductCategoriessIDProperty); }
        }

        public static PropertyInfo<String> CategoryNameProperty = RegisterProperty<String>(c => c.CategoryName, "Category Name", "");
        /// <summary>
        /// Gets and sets the Category Name value
        /// </summary>
        [Display(Name = "Category Name", Description = ""),
        StringLength(25, ErrorMessage = "Category Name cannot be more than 25 characters")]
        public String CategoryName
        {
            get { return GetProperty(CategoryNameProperty); }
            set { SetProperty(CategoryNameProperty, value); }
        }

        #endregion

        #region " Methods "

        protected override object GetIdValue()
        {
            return GetProperty(ProductCategoriessIDProperty);
        }

        public override string ToString()
        {
            if (this.CategoryName.Length == 0)
            {
                if (this.IsNew)
                {
                    return String.Format("New {0}", "Product Categories");
                }
                else
                {
                    return String.Format("Blank {0}", "Product Categories");
                }
            }
            else
            {
                return this.CategoryName;
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
            // Set any variables here, not in the constructor or NewProductCategories() method.
        }

        public static ProductCategories NewProductCategories()
        {
            return DataPortal.CreateChild<ProductCategories>();
        }

        public ProductCategories()
        {
            MarkAsChild();
        }

        internal static ProductCategories GetProductCategories(SafeDataReader dr)
        {
            var p = new ProductCategories();
            p.Fetch(dr);
            return p;
        }

        protected void Fetch(SafeDataReader sdr)
        {
            using (BypassPropertyChecks)
            {
                int i = 0;
                LoadProperty(ProductCategoriessIDProperty, sdr.GetInt32(i++));
                LoadProperty(CategoryNameProperty, sdr.GetString(i++));
            }

            MarkAsChild();
            MarkOld();
            BusinessRules.CheckRules();
        }

        protected override Action<SqlCommand> SetupSaveCommand(SqlCommand cm)
        {
            AddPrimaryKeyParam(cm, ProductCategoriessIDProperty);

            cm.Parameters.AddWithValue("@CategoryName", GetProperty(CategoryNameProperty));

            return (scm) =>
            {
    // Post Save
    if (this.IsNew)
                {
                    LoadProperty(ProductCategoriessIDProperty, scm.Parameters["@ProductCategoriessID"].Value);
                }
            };
        }

        protected override void SaveChildren()
        {
            // No Children
        }

        protected override void SetupDeleteCommand(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@ProductCategoriessID", GetProperty(ProductCategoriessIDProperty));
        }

        #endregion

    }

}