namespace ${model.NameSpace}.${dbSetting.Name}
{

	/// <summary>
    /// ${table.Name}
    /// <remarks>
    /// ${table.Description}
    /// </remarks>
    /// createtime : ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    /// </summary>
    public partial class ${table.Name}
    {
        
		$foreach(column in table.Columns)   $if(helper.IsNotEnumType(column) != true)
		/// <summary>
        /// $column.Description
        /// </summary>
        public virtual ${column.Table.Name}${column.Name}Type $column.Name { get; set; }    $end	$end
		
    }
	
	
	$helper.CreateEnumTypes(table)
	
    
}