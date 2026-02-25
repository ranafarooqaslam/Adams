<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptPartyActivity.aspx.cs" Inherits="Forms_RptPartyActivity" Title="SAMS :: Party Activity Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">    
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script type="text/javascript">
    function pageLoad() {

        $("select").searchable();
    }
</script>
    <div id="right_data">
        <table>
            <tr>
                <td>
                    <div id="divUpdatePanel">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Report Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="250px">
                                                    <asp:ListItem Value="0" Text="Summary" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Detail"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="240px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Customer Route"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="240px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblSaleForce" runat="server" Width="79px" Text="Sale Force"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="ddlSaleForce" runat="server" Width="240px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblNickName" runat="server" Width="79px" Text="Channel Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpChannelType" runat="server" Width="240px" AutoPostBack="true" OnSelectedIndexChanged="drpChannelType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblCustomer" runat="server" Width="79px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="240px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblFromDate" runat="server" Width="76px" Height="13px" Text="From Date"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                    Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>                
                                                <div style="z-index: 101; left: 350px; width: 100px; position: absolute; top: 245px;
                                                    height: 100px">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                            <ProgressTemplate>
                                                                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Width="31px" Height="33px"
                                                                    ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </asp:Panel>
                                                </div>                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblDateTo" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px"
                                                    CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                                                    PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                                    PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnPDF_Click" />
                    <asp:Button ID="btnExcel" runat="server" CssClass="Button" Text="View Excel" Width="90"
                        OnClick="btnExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
