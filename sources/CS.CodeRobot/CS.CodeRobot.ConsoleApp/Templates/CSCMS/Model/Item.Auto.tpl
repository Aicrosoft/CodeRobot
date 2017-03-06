using System;

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改，可枚举且可为Null的字段会在首次生成时在可编辑的部分类中
// autogeneration ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") } powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------

namespace ${model.NameSpace}.${dbSetting.Name}
{
    /// <summary>
    /// ${table.Description}
    /// </summary>
    [Serializable]
    partial class ${table.Name}
    {
		
        $foreach(column in table.Columns)  $if(helper.IsNotEnumType(column))
		/// <summary>
        /// $column.Description
        /// </summary>
        public virtual $helper.ToDotNetType(column) $column.Name { get; set; }
		$end  $end
		
		
    }
    
}
