<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorAssignment.aspx.cs" Inherits="Forms_frmDistributorAssignment"
    Title="SAMS :: Location Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td style="width: 100px; height: 363px;">
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 82px; height: 17px">
                                            <asp:Label ID="lblmsg" runat="server" Width="114px" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td style="width: 216px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="107px" Text="Select User" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 216px; height: 10px">
                                            <asp:DropDownList ID="ddRole" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="ddUser_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="112px" Text="Location Type" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 216px; height: 13px">
                                            <asp:DropDownList ID="ddDistributorType" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100px" rowspan="4">
                                                            <asp:ListBox ID="lstUnAssignDistributor" runat="server" Width="150px" Height="200px"
                                                                CssClass="DropList"></asp:ListBox>
                                                        </td>
                                                        <td align="center">
                                                        </td>
                                                        <td style="width: 102px" rowspan="4">
                                                            <asp:ListBox ID="lstAssignDistributor" runat="server" Width="150px" Height="200px"
                                                                CssClass="DropList"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="Button3" OnClick="Button3_Click" runat="server" Width="30px" CssClass="Button"
                                                                Text=">"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="Button4" OnClick="Button4_Click" runat="server" Width="30px" CssClass="Button"
                                                                Text="<"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="1">
                                                        </td>
                                                        <td style="width: 75px; height: 24px">
                                                        </td>
                                                        <td rowspan="1">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 100px; height: 363px;">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Width="28px" Height="22px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                            </asp:ImageButton>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
