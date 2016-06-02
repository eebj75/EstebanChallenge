<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HPChallenge.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
      <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <asp:label runat="server" text="Upload File"></asp:label>
    <asp:FileUpload id="FileUploadControl" runat="server" />
    <asp:button runat="server" id="btnUploadFile" text="Upload" OnClick="btnUploadFile_Click" />
    <br /><br />
    <asp:label runat="server" text="Get top Players: "></asp:label>
    <asp:textbox runat="server" id="txtCount" width="80px" MaxLength="4" text="10" onkeypress="return isNumberKey(event)"></asp:textbox>
    <asp:button runat="server" id="btnGetTopScores" text="Get Data" OnClick="btnGetTopScores_Click" />
    <div id="divTopScores" runat="server">
        <asp:gridview runat="server" id="gvTopScores">
        </asp:gridview>
    </div>
</asp:Content>
