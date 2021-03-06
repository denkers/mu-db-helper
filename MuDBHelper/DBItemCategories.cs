﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuDBHelper
{
    [Table(Name="DBHelper_ItemCategories")]
    class DBItemCategories
    {
        [Column(IsPrimaryKey=true)]
        public int ID { get; set; }

        [Column]
        public string name { get; set; }

        public string toString()
        {
            return name;
        }

        public static DBItemCategories findCategory(int categoryID)
        {
            using(DBConnection conn = new DBConnection())
            {
                return conn.categories.Where(c => c.ID == categoryID).FirstOrDefault();
            }
        }
    }

}
