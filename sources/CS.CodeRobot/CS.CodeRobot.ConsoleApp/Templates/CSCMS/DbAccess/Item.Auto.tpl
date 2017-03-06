$set(modelClassName = table.Name)$set(clsDao = table.Name + model.Suffix)$set(clsDbContext = dbSetting.DbContextName)
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ${pi.Namespace}.Model.${dbSetting.Name};

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改。
// autogeneration ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") } powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------

namespace ${model.NameSpace}.${dbSetting.Name}
{
    
	/// <summary>
    /// ${table.Name}
    /// <remarks>
    /// ${table.Description}
    /// </remarks>
    /// createtime : ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    /// </summary>
    partial class $clsDao
    {
        /// <summary>
        /// 主键获取
        /// </summary>
        /// <returns></returns>
        public virtual $modelClassName Get(${helper.GetPkQueryParams(table)})
        {
            using (var db = new ${clsDbContext}())
            {
                var item = db.${modelClassName}.FirstOrDefault(${helper.GetQueryExpressionByPk(table)});
                return item;
            }
        }
		
		/// <summary>
        /// 获取默认的第一个       
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual $modelClassName GetFirstOrDefault(Func<$modelClassName, bool> predicate)
        {
            using (var db = new ${clsDbContext}())
            {
                var item = db.${modelClassName}.FirstOrDefault(predicate);
                return item;
            }
        }

        /// <summary>
        /// 条件查询获取
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<$modelClassName> Get(Func<$modelClassName, bool> predicate)
        {
            using (var db = new ${clsDbContext}())
            {
				var items = db.${modelClassName}.Where(predicate).ToList();
                return items;
            }
        }
		
		 /// <summary>
        /// 保存一个
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int Save($modelClassName item)
        {
            using (var db = new ${clsDbContext}())
            {
                db.${modelClassName}.AddOrUpdate(item); //效率低
                var val = db.SaveChanges();
                return val;
            }
        }

        /// <summary>
        /// 保存多个,性能较差
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public virtual int Save(IEnumerable<$modelClassName> items)
        {
            using (var db = new ${clsDbContext}())
            {
                foreach (var item in items)
                {
                    db.${modelClassName}.AddOrUpdate(item); //效率低
                }
                var val = db.SaveChanges();
                return val;
            }
        }

        public virtual int Insert($modelClassName o)
        {
            using (var db = new ${clsDbContext}())
            {
                db.${modelClassName}.Add(o);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 批插入
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkInsert(IEnumerable<$modelClassName> items)
        {
            using (var db = new ${clsDbContext}())
            {
                db.BulkInsert(items);
            }
        }

        public virtual int Update($modelClassName o)
        {
            using (var db = new ${clsDbContext}())
            {
                db.${modelClassName}.AddOrUpdate(o);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 批更新
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkUpdate(IEnumerable<$modelClassName> items)
        {
            using (var db = new ${clsDbContext}())
            {
                db.BulkUpdate(items);
            }
        }

        /// <summary>
        /// 批删除
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkDelete(IEnumerable<$modelClassName> items)
        {
            using (var db = new ${clsDbContext}())
            {
                db.BulkDelete(items);
            }
        }

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="items"></param>
        public virtual int Insert(IEnumerable<$modelClassName> items)
        {
            using (var db = new ${clsDbContext}())
            {
                foreach (var item in items)
                    db.${modelClassName}.Add(item); //Add会忽略主键的(值)影响,即内容一定会增加到DB中去

                var val = db.SaveChanges();
                return val;
            }
        }

        /// <summary>
        /// 删除某对象
        /// </summary>
        /// <param name="o"></param>
        public virtual int Delete($modelClassName o)
        {
            using (var db = new ${clsDbContext}())
            {
                var items = db.$modelClassName .Where(x => ${helper.GetPkExpress(table,"o")});
                foreach (var item in items)
                    db.${modelClassName}.Remove(item);

                var val = db.SaveChanges();
                return val;
            }
        }

        
    }
}