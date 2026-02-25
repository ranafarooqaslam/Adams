<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="rptLoadPass2.aspx.cs" Inherits="Forms_rptLoadPass2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
<script language="JavaScript" type="text/javascript">
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
                                            <strong>
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left" colspan="1">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 1px; height: 1px" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 25px;">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Report Type" ></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px;">
                                        </td>
                                        <td align="center" style="height: 25px">
                                             <asp:RadioButtonList ID="rdbReportType" runat="server" Width="200px"  RepeatDirection="Horizontal">
                                            <asp:ListItem Text="DN Issue"  Selected ="True" Value="0" ></asp:ListItem>
                                             <asp:ListItem Text=" DN Close" Value="1" ></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left" style="height: 25px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 25px;">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Location" 
                                                    Enabled="False"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px;">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="210px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="height: 25px">
                                        </td>
                                    </tr>
                                      
                                    <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Text="Principal" ></asp:Label>
                                            </strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="210px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                   
                                            <asp:Label ID="lblfromLocation" runat="server" Text="Route" CssClass="lblbox" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="210px" CssClass="DropList"
                                            AutoPostBack="True" Visible="false" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                   
                                    <tr>
                                        <td style="width: 1px;" align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="lblSalesMan" runat="server"  Text="Deliveryman"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpOrderBooker" runat="server" Width="210px">
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
                                                <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="From Date" ></asp:Label>
                                            </strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtBox" MaxLength="11" Width="153px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="To Date" Width="59px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="11" Width="153px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>--%>
                                </tbody>
                            </table>
                            <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                         <%--   <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>--%>
                            &nbsp;
                         </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;<asp:Button ID="btnPDF" runat="server" Text="View PDF" CssClass="Button"  Width="90px"
                        onclick="btnPDF_Click"  />
                    <asp:Button ID="Button1" runat="server" Text="View Excel" CssClass="Button" OnClick="Button1_Click" Width="90px"/>
                   
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
