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
    <br />
    <div id="divFileUpload">
         <asp:label runat="server" text="Upload File "></asp:label>
         <asp:FileUpload id="FileUploadControl" runat="server" />
         <asp:button runat="server" id="btnUploadFile" text="Upload" OnClick="btnUploadFile_Click" />
    </div>
    <br /><br />
    <asp:label runat="server" text="Get top "></asp:label>
    <asp:textbox runat="server" id="txtCount" width="40px" MaxLength="4" text="10" onkeypress="return isNumberKey(event)"></asp:textbox> Players
    <asp:button runat="server" id="btnGetTopScores" text="Get Data" OnClick="btnGetTopScores_Click" />
    <br /><br />
    <div id="divTopScores" runat="server">
        <asp:gridview runat="server" id="gvTopScores" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:gridview>
    </div>
</asp:Content>
