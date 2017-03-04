
<div class="layout">
    <h1>
        {% block title %}
        Default title
        {% endblock %}
    </h1>
    <p>
        {% block body %}
        Default body
        {% endblock %}
    </p>
</div>

如名：{{ m.Name }}
年纪：{{ m.Age }}


Hello, {% include "~/Templates/include.tpl" %}!

哈哈哈

<html>


其他信息: Liquid Error - Only 'comment' and 'block' tags are allowed in an extending template


