<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptOutletWiseSale.aspx.cs" Inherits="Forms_rptOutletWiseSale" Title="SAMS :: Catagory Wise Customer Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script type="text/javascript">
    function pageLoad() {

        $("select").searchable();
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
                                    
                                    <td>
                                    
                                    
                                    </td>
                                    <td> <strong>
                                                <asp:Label ID="Label6" runat="server" Width="78px" Text="Report Type" CssClass="lblbox"></asp:Label></strong></td>
                                    <td></td>
                                      <td style="height: 25px; width: 1px;" align="left" colspan="2">
                                            <asp:RadioButtonList ID="RbReportType" runat="server" 
                                                AutoPostBack="True" RepeatDirection="Horizontal" Height="16px" 
                                                Width="254px">
                                                <asp:ListItem Selected="True">Product Wise</asp:ListItem>
                                                <asp:ListItem>Value Wise</asp:ListItem>
                                                  <asp:ListItem>Both</asp:ListItem>
                                              
                                            </asp:RadioButtonList>
                                        </td></tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 1px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 1px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="48px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>


                                      <tr>
                                       <td></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Customer Route"
                                                    CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                     <td></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged"
                                                AutoPostBack="True" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblCustomer" runat="server" Width="79px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td></td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                   
                                   
                                    <tr>
                                            <td align="left">
                                            </td>
                                            
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="78px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpSaleForce" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="48px" Text="Catagory" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="width: 204px; height: 2px" align="left">
                                            <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="All Catagories" AutoPostBack="True"
                                                OnCheckedChanged="ChbAllLocationType_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListCatagory" runat="server" AutoPostBack="True">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                        </td>
                                    </tr>

                                  
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left" colspan="2">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server"   Text="From Date"
                                                    CssClass="lblbox" Width="96px"></asp:Label></strong>
                                        </td>
                                        
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 1px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="96px" Text="To Date" 
                                                CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                EnableViewState="False" PopupButtonID="ImgBntFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                EnableViewState="False" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
