@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Add 2 numbers</h2>
<form method="get" id="adder">
    <p>
        <label for="number1">Number1</label><br />
        <input id="number1" type="text" name="number1" value="@Model.num1"/>
    </p>
    <p>
        <label for="number2">Number2</label><br />
        <input id="number2" type="text" name="number2" value="@Model.num2"/>
    </p>
    <p>
        <label for="result">Sum</label><br />
        <span>@Model.Sum()</span>
    </p>
    <button type="submit">Add</button>
</form>
<script>
    jQuery(document).ready(function () {
        jQuery("#adder").submit(function () {
            var sVal = jQuery("#number1").val();
            if (sVal === "") jQuery("#number1").val(20);
            // let it do the default action (don't return false)
        });
    });

</script>
