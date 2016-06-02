<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HPChallenge.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="By Clicking this button all the scores data will be deleted  "></asp:Label>
    <asp:Button ID="btnDeleteScores" runat="server" OnClick="btnDeleteScores_Click" Text="Delete Scores" />
</asp:Content>
