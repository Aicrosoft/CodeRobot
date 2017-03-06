namespace CS.DataSchema
{
    public class DbColumn: NamedObject
    {
        /// <summary>
        /// Gets or sets the database data type as a string (as defined by the database platform)
        /// </summary>
        /// <value>
        /// The type of the db data.
        /// </value>
        public string DbDataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if nullable; otherwise, <c>false</c>.
        /// </value>
        public bool Nullable { get; set; }

        /// <summary>
        /// Gets or sets the ordinal (the order that the columns were defined in the database).
        /// </summary>
        /// <value>
        /// The ordinal.
        /// </value>
        public int Ordinal { get; set; }


        /// <summary>
        /// Gets or sets a default value for the column. May be null.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public string DefaultValue { get; set; }


        /// <summary>
        /// Gets or sets the name of the parent table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }

    }
}