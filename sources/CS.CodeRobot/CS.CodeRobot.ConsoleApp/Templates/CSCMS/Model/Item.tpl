namespace ${model.NameSpace}.${dbSetting.Name}
{

	/// <summary>
    /// ${table.Name}-${table.Description}
    /// <remarks>
    /// 
    /// </remarks>
    /// createtime : ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    /// </summary>
    public partial class ${table.Name}
    {
        
		$foreach(column in table.Columns)
		$if(!helper.IsNotEnumType(column))
		/// <summary>
        /// $column.Description
        /// </summary>
        public virtual $helper.ToDotNetType(column) $column.Name { get; set; } $end
				
		$end
		
		
		
		
    }
	
	
	$helper.CreateEnumTypes(table)
	
    
}