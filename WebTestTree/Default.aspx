<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTestTree._Default" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Безымянная страница</title>
    <style type="text/css">
        .style1
        {
            width: 446px;
        }
        .style2
        {
            width: 410px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PanelView" runat="server">
            <table style="width:100%;">
                <tr>
                    <td rowspan="2">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:HyperLink ID="AddLink" runat="server">Добавить</asp:HyperLink>
                        &nbsp;|
                        <asp:HyperLink ID="EditLink" runat="server">Редактировать</asp:HyperLink>
                        |<asp:HyperLink ID="RemoveLink" runat="server">Удалить</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="2">
                        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" 
                            Height="209px" Width="553px">
                            <ItemTemplate>
                                <h1>
                                    <asp:Label ID="titleLabel" runat="server" Text='<%# Bind("title") %>' />
                                </h1>
                                <asp:Label ID="contentLabel" runat="server" Text='<%# Bind("content") %>' />
                                <br />
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:Panel ID="AddPanel" runat="server" Height="287px">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="nameLabel" runat="server" Text="Название страницы"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nameTextBox" runat="server" Width="424px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="nameTextBox" ErrorMessage="Вы не ввели название страницы"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ControlToValidate="nameTextBox" 
                                            ErrorMessage="Название страницы должно сотоять из символов [a-zA-z0-9]"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Заголовок страницы"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="titleTextBox" runat="server" style="height: 21px" 
                                            Width="421px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="titleTextBox" ErrorMessage="Вы не ввели заголовок страницы"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Содержание"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="descriptionTextBox" runat="server" Height="102px" Rows="10" 
                                            Width="420px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="descriptionTextBox" 
                                            ErrorMessage="Вы не ввели содержание страницы"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="CreateButton" runat="server" onclick="CreateButton_Click" 
                                            Text="Создать" Width="415px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
            <asp:HiddenField ID="HiddenFieldId" runat="server" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:PagesConnectionString %>" 
                
                SelectCommand="SELECT [name], [title], [content] FROM [Pages] WHERE ([id] = @id)" 
                UpdateCommand="UPDATE Pages SET title = @Param1, [content] = @Param2  WHERE ([id] = @id)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenFieldId" DefaultValue="" Name="id" 
                        PropertyName="Value" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Param1" />
                    <asp:Parameter Name="Param2" />
                    <asp:Parameter Name="id" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </asp:Panel>
       
    
    </div>
 

    </form>
</body>
</html>
