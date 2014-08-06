vbmvc4add2numbers
=================

A student of mine got her first coding job doing vb.net and razor mvc. We covered mvc using c#, but not vb in class. This is an attempt to vbize some of the examples that I have used in class.

We start with a form:

    <form method="get" id="adder">
        <p>
            <label for="number1">Number1</label><br />
            <input id="number1" type="text" name="number1"/>
        </p>
        <p>
            <label for="number2">Number2</label><br />
            <input id="number2" type="text" name="number2"/>
        </p>
        <p>
            <label for="result">Sum</label><br />
            <span></span>
        </p>
        <button type="submit">Add</button>
    </form>

The form "works" but it doesn't actually do anything yet. We will wrap it in mvc to make it do something. First we create a new vb/web/mvc basic solution. It will contain Model, Views and Controllers folders. Our new form is a view. In the Views folder, create a subfolder called `Home` to put our new view in. Name the view `index.vbhtml`. Visual studio will scaffold you something that looks like this:

    @Code
        ViewData("Title") = "Index"
        Layout = "~/Views/Shared/_Layout.vbhtml"
    End Code
    
    <h2>Title</h2>
  
You will paste the form into the view like this:

    @Code
        ViewData("Title") = "Index"
        Layout = "~/Views/Shared/_Layout.vbhtml"
    End Code
    
    <h2>Add 2 numbers</h2>
    <form method="get" id="adder">
          <p>
              <label for="number1">Number1</label><br />
              <input id="number1" type="text" name="number1"/>
          </p>
          <p>
              <label for="number2">Number2</label><br />
              <input id="number2" type="text" name="number2"/>
          </p>
          <p>
              <label for="result">Sum</label><br />
              <span></span>
          </p>
          <button type="submit">Add</button>
      </form>
    

If you hit the easy button in Visual Studio, you will get a 404 error, same as if you were working in c#. Why? Because you need a controller. Right mouse over the Controllers folder and select Add/Controller. Name the controller HomeController. Visual Studio will scaffold you something that looks like this:

    Public Class HomeController
        Inherits System.Web.Mvc.Controller
    
        '
        ' GET: /Home
    
        Function Index() As ActionResult
            Return View()
        End Function
    
    End Class
  
Now if you click the easy button, you should see your form. Shazam! So far we have the view and controller, to add behaviour we will model that behaviour with a vb class. You add the class by right mousing over your Models folder and clicking AddClass. Name the class Add.vb. It will scaffold you something that looks like this:

    Public Class Add

    End Class

You will add behaviour to the class:

    Public Class Add
        Public num1 = 0
        Public num2 = 0
    
        Public Function Sum()
            Return num1 + num2
        End Function
    
    
    End Class
    
Now all that remains is to connect the class to the controller:

    Function Index() As ActionResult
        Dim add = New Add()
        add.num1 = 0
        add.num2 = 0
        Try
            add.num1 = Integer.Parse(Request("number1"))
            add.num2 = Integer.Parse(Request("number2"))
        Catch ex As Exception
            ' ignore error
        End Try
        Return View(add)
    End Function

The addends are from inputs named `number1` and `number2`. The inputs are text inputs so, anything can be keyed in there. To convert the text to numbers we use `Integer.Parse(Request("number1"))`. Because anything can be keyed there we enclose this in a try with an empty catch. All we need to do to display the sum in the view is to put `Model.Sum()` in the span for the result:

    <p>
        <label for="result">Sum</label><br />
        <span>@Model.Sum()</span>
    </p>
 
Now if you hit the easy button, you should be able to add 2 numbers together. The user experience isn't very good though, because you have to remember the addends. To display the addends with the result you can change the view to be:

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

Just for the finishing touch on the user experience, lets say that I want to put the value 20 in the first number if it is blank. I could do that on the server side, but then the input would start out with 20 in it. I only want 20 in there if the user leaves the input blank or 0. To do this, I can use a little jQuery:

    <script>
        jQuery(document).ready(function () {
            jQuery("#adder").submit(function () {
                var sVal = jQuery("#number1").val();
                if (sVal < 1) jQuery("#number1").val(20);
                // let it do the default action (don't return false)
            });
        });
    
    </script>
    
Oops, that little bit of jQuery doesn't run. If I open the console I see `jQuery is undefined`. To fix that, I need to go into the Views/Shared folder and edit _layout.vbhtml so that the jQuery bundle is under modernizer like this:

    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width" />
        <title>@ViewData("Title")</title>
        @Styles.Render("~/Content/css")
        @Scripts.Render("~/bundles/modernizr")
        @Scripts.Render("~/bundles/jquery")
    </head>
    <body>
        @RenderBody()
    
        @RenderSection("scripts", required:=False)
    </body>
    </html>
    
In conclusion, there are a lot of programmers that find vb.net less daunting then c#. The combination of vb.net and mvc allows us to program in a standards compliant way that is perhaps also familiar on the server side. More examples to come!
