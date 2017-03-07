
{%- for sub in subs -%}
{{- sub -}} 项目的相关处理
{{- sub | assembly }} {{-sub}}  AssemblyFile 生成处理完成
{{- sub | configs }} {{-sub}}  *.config 拷贝生成处理完成
{{- sub | projetcs }} {{- sub}}  *.project 拷贝文件生成处理完成

{%- endfor -%}

