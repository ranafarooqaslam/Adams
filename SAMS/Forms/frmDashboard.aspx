<%@ Page Title="SAMS: Dashboard" Language="C#" MasterPageFile="~/Forms/DashboardMaster.master" AutoEventWireup="true" CodeFile="frmDashboard.aspx.cs" Inherits="Forms_frmDashboard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="DevExpress.Dashboard.v16.1.Web, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainCopy" Runat="Server">
<link href="../css/styleDashBoard.css" rel="stylesheet" type="text/css" />
<style>
.ajax__calendar .ajax__calendar_container {
border: 1px solid #646464;
background-color: #ffffff;
color: #000000;
z-index: 100;
}
</style>
<div id="right_data">
<asp:UpdatePanel runat="server" id="UpdatePanel1" 
    UpdateMode="Conditional">
  <contenttemplate>    
    <table width="100%">
        <tr>
            <td style="width:16%">
                <strong>
                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                </strong>
                <br />
                <asp:TextBox ID="txtDate" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ibDate" runat="server" 
                ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                <cc1:CalendarExtender ID="ceDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibDate"
                TargetControlID="txtDate">
            </cc1:CalendarExtender>
            </td>
            <td style="width:21%">
                <strong>
                    <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                </strong>
                <br />
                <asp:DropDownList ID="ddlRegion" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:21%">
                <strong>
                    <asp:Label ID="lblZone" runat="server" Text="Area"></asp:Label>
                </strong>
                <br />
                <asp:DropDownList ID="ddlZone" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="ddlZone_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:21%">
                <strong>
                    <asp:Label ID="lblTerritory" runat="server" Text="Territory"></asp:Label>
                </strong>
                <br />
                <asp:DropDownList ID="ddlTerritory" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="ddlTerritory_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width:21%">
                <strong>
                    <asp:Label ID="lblLocation" runat="server" Text="Distributor"></asp:Label>
                </strong>
                <br />
                <asp:DropDownList ID="ddlLocation" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                </asp:DropDownList>
            </td>       
        </tr>
    </table>
      <div>
        <dx:ASPxDashboardViewer ID="dshViewer" runat="server" Width="95%" Height="700px"
            onDashboardLoading="dshViewer_DashboardLoading" 
            OnDataLoading="dshViewer_DataLoading">
        </dx:ASPxDashboardViewer>
         </div>
  </contenttemplate>
</asp:UpdatePanel>
</div>

</asp:Content>