using System;
using Singular.CommonData;

namespace MELib
{
    public class CommonData : CommonDataBase<MELib.CommonData.MECachedLists>
    {
        [Serializable]
        public class MECachedLists : CommonDataBase<MECachedLists>.CachedLists
        {
            /// <summary>
            /// Gets cached ROUserList
            /// </summary>
            public MELib.RO.ROUserList ROUserList
            {
                get
                {
                    return RegisterList<MELib.RO.ROUserList>(Misc.ContextType.Application, c => c.ROUserList, () => { return MELib.RO.ROUserList.GetROUserList(); });
                }
            }
            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            public RO.ROMovieGenreList ROMovieGenreList
            {
                get
                {
                    return RegisterList<MELib.RO.ROMovieGenreList>(Misc.ContextType.Application, c => c.ROMovieGenreList, () => { return MELib.RO.ROMovieGenreList.GetROMovieGenreList(); });
                }
            }
            public RO.ProductCategoriesList ROProductCategorieList
            {
                get
                {
                    return RegisterList<MELib.RO.ProductCategoriesList>(Misc.ContextType.Application, c => c.ROProductCategorieList, () => { return MELib.RO.ProductCategoriesList.GetProductCategoriesList(); });
                }
            }

            /// <summary>
            /// Gets cached ROMovieGenreList
            /// </summary>
            /// 


            public Maintenance.ProductCategorieList ProductCategorieList
            {
                get
                {
                    return RegisterList<MELib.Maintenance.ProductCategorieList>(Misc.ContextType.Application, c => c.ProductCategorieList, () => { return MELib.Maintenance.ProductCategorieList.GetProductCategorieList(); });
                }
            }

            public Delivery.DeliveryTypeList DeliveryTypeList
            {
                get
                {
                    return RegisterList<MELib.Delivery.DeliveryTypeList>(Misc.ContextType.Application, c => c.DeliveryTypeList, () => { return MELib.Delivery.DeliveryTypeList.GetDeliveryTypeList(); });
                }
            }

            public Transaction.TransactionTypeList TransactionTypeList
            {
                get
                {
                    return RegisterList<MELib.Transaction.TransactionTypeList>(Misc.ContextType.Application, c => c.TransactionTypeList, () => { return MELib.Transaction.TransactionTypeList.GetTransactionTypeList(); });
                }
            }
        }
    }

    public class Enums
    {
        public enum AuditedInd
        {
            Yes = 1,
            No = 0
        }
        public enum DeletedInd
        {
            Yes = 1,
            No = 0
        }
    }
}
