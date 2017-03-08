//init the codefiles
{{sub | init_code_files }}

//Loop DbConns
{{"正在读取DatabaseSchema信息，请不要中断程序..." | out}}
{%- for ds in pi.DbConns -%}
{{sub | render:"DbContent.tpl",ds.DbContextName,true,ds.Name}}

{{ds.Name}}  -  {{ds.DbConnName}}  {{ds.DbContextName}}

{%- for tn in ds.TableNames -%}

{{sub | render_table_item:ds.Name,tn}}


{%- endfor -%}

{%- endfor -%}

{{sub | update_project}} //更新工程项目



