<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_rptLoan.aspx.cs" Inherits="pr_rptLoan"
    MasterPageFile="~/Forms/PageMaster.master" Title="Loan  / Lease Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
<script language="javascript" type="text/javascript">
    function onCalendarShown3() {
        var cal = $find("calendar3");
        cal._switchMode("months", true);
        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call3);
                }
            }
        }
    }

    function onCalendarHidden3() {
        var cal = $find("calendar3");

        if (cal._monthsBody) {
            for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                var row = cal._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call3);
                }
            }
        }
    }

    function call3(eventElement) {
        var target = eventElement.target;
        switch (target.mode) {
            case "month":
                var cal = $find("calendar3");
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }
    }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" colspan="1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlLocation" runat="server" Width="210px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="height: 25px;" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Department" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="DropList" Width="209px" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Employee" Width="61px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="210px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Report Type" Width="90px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="ddlType" runat="server" Width="210px" AutoPostBack="true" 
                                                onselectedindexchanged="ddlType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="lblAssetsType" runat="server" Text="Assets Type" Width="90px" Visible="false"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="ddlAssetsType" runat="server" Width="210px" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="59px" Text="For Month"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                           <asp:TextBox ID="txtFromDate" runat="server" Width="153px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>                                                    
                                </tbody>
                            </table>               
                            <cc1:CalendarExtender id="CEStartMonth" runat="server" BehaviorID="calendar3" OnClientShown="onCalendarShown3" OnClientHidden="onCalendarHidden3" Format="MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="Button"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
