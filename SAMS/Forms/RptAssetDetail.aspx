<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptAssetDetail.aspx.cs" Inherits="Forms_RptAssetDetail" Title="SAMS :: Asset Detail Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<style>
   .ajax__calendar_container { z-index : 1000 ; }
</style>

<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script language="JavaScript" type="text/javascript">

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }

        function ValidateForm() {
           <%-- var str;
            str = document.getElementById('<%=txtAssetType.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Asset Type');
                return false;
            }--%>
            return true;
        }
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";

        }
        function pageLoad() {
            $("select").searchable();
            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
    <div id="right_data">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table cellspacing="10px" cellpadding="2px">
                                    <tbody>
                                        <tr>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="78px" Text="Asset Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 204px; height: 2px" align="left">
                                                <asp:CheckBox ID="ChbAllAssetType" runat="server" Text="All Asset Type" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllAssetType_CheckedChanged"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblSkus" runat="server" Width="58px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                                <asp:CheckBox ID="ChbAllLocation" runat="server" Text="All Location" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllLocation_CheckedChanged"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListAssetType" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                          <asp:Panel ID="Panel2" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListLocation" AutoPostBack="true" OnSelectedIndexChanged="ListLocation_SelectedIndexChanged" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                         <tr>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="88px" Text="Asset No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 204px; height: 2px" align="left">
                                                <asp:CheckBox ID="ChbAllAssetNo" runat="server" Text="All Asset No" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllAssetNo_CheckedChanged"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="98px" Text="Transaction Type" CssClass="lblbox"></asp:Label></strong>
                                                <asp:CheckBox ID="ChbAllTransactionType" runat="server" Text="All Transaction Type" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllTransactionType_CheckedChanged"></asp:CheckBox>
                                            </td>
                                        </tr>

                                        <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListAssetNo" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                          <asp:Panel ID="Panel4" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListTransactionType" runat="server" AutoPostBack="True">
                                                    <asp:ListItem Text="Purchase" Value="111"></asp:ListItem>
                                                    <asp:ListItem Text="Transfer Out" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Transfer In" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Transfer Out To Customer" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Return From Customer" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Damage" Value="3"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                         <tr>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="88px" Text="Chiller No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 2px" align="left"></td>
                                            <td style="width: 204px; height: 2px" align="left">
                                                <asp:CheckBox ID="ChbAllChillerNo" runat="server" Text="All Chiller No" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllChillerNo_CheckedChanged"></asp:CheckBox>
                                            </td>
                                           <td style="width: 1px; height: 2px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="98px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                                <asp:CheckBox ID="ChbAllCustomer" runat="server" Text="All Customer" AutoPostBack="True"
                                                    OnCheckedChanged="ChbAllCustomer_CheckedChanged"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel5" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListChillerNo" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                           <asp:Panel ID="Panel6" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListCustomer" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                         <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="70px" Height="13px" Text="From Date"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                                                PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                                PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br /> &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnPDF" runat="server" Text="View PDF" CssClass="Button"  Width="90px"
                        onclick="btnPDF_Click"  />
                    <asp:Button ID="Button1" runat="server" Text="View Excel" CssClass="Button" OnClick="Button1_Click" Width="90px"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
