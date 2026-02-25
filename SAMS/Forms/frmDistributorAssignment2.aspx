<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorAssignment2.aspx.cs" Inherits="Forms_frmDistributorAssignment2"
    Title="SAMS :: Distributor Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <div style="z-index: 101; left: 481px; width: 100px; position: absolute; top: 282px; height: 100px"
            id="DIV1">
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="28px" Height="22px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <strong>Location</strong>&nbsp;&nbsp;
                                              <asp:DropDownList ID="ddDistributorType" runat="server" Width="200px" CssClass="DropList"
                                                  OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged" AutoPostBack="True">
                                              </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td style="width:30%">
                            <strong>UnAssigned Distributors</strong>
                        </td>
                        <td style="width:15%"></td>
                        <td style="width:55%">
                            <strong>Assigned Distributors</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <asp:ListBox ID="lstUnAssignDistributor" runat="server" Width="320px" Height="300px"></asp:ListBox>
                        </td>
                        <td style="width:15%" align="center" valign="middle">
                            <asp:Button ID="Button3" OnClick="Button3_Click" runat="server" Width="30px" CssClass="Button" Text=">"></asp:Button>
                            <br />
                            <br />
                            <asp:Button ID="Button4" OnClick="Button4_Click" runat="server" Width="30px" CssClass="Button" Text="<"></asp:Button>
                        </td>
                        <td style="width:55%">
                            <asp:ListBox ID="lstAssignDistributor" runat="server" Width="320px" Height="300px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
