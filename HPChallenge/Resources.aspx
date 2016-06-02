<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="HPChallenge.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    Please select any file from the list for download.
    <br /><br />
    <a href="TestFiles/HPTestFile1.txt" target="_blank">Test File 1</a>
    <br />
    <a href="TestFiles/HPTestFile2.txt">Test File 2</a>
    <br />
    <a href="TestFiles/HPTestFile3.txt">Test File 3</a>
    <br /><br /><br />
    Source Code available here: 
    <a href="https://github.com/eebj75/HPChallenge">https://github.com/eebj75/HPChallenge</a>

</asp:Content>
