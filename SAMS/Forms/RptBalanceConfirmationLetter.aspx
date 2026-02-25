<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptBalanceConfirmationLetter.aspx.cs" Inherits="Forms_RptBalanceConfirmationLetter"
    Title="SAMS :: Customer Invoice Wise Sales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }


        function SelectAllCheckboxesSpecific(spanChk) {


            var IsChecked = spanChk.checked;
            var Chk = spanChk;
            Parent = document.getElementById('<%= Grid_users.ClientID %>');

            var items = Parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {
                if (items[i].id != Chk && items[i].type == "checkbox") {
                    if (items[i].checked != IsChecked) {

                        items[i].click();
                    }
                }
            }
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Report Type"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="200">
                                                <asp:ListItem Text="Summary" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Detail" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropList" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Principal" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Channel" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="drpChannelType" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Customer Type"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="ddlType" runat="server" Width="200px">
                                                <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Registered" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Non-registered" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Height="13px" Text="Contact Person"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtContactPerson" runat="server" Width="200px"></asp:TextBox>                                            
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Height="13px" Text="Designation"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtDesignation" runat="server" Width="200px"></asp:TextBox>                                            
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="As On Date" Width="77px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                                Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>                                    
                                     <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                </table>
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                    width: 100%; border-bottom: silver thin inset; background-color: silver">
                                    <tbody>
                                        <tr>
                                        <td style="width: 97px"> <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="Select All" onclick="SelectAllCheckboxesSpecific(this);">
                        </asp:CheckBox></td>
                                            <td style="height: 21px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="157px" 
                                                    Text="Select Searching Type"></asp:Label></strong>
                                            </td>
                                            <td style="width: 170px; height: 21px" align="left">
                                                <asp:DropDownList ID="ddSearchType" runat="server" Width="195px" CssClass="DropList">
                                                    <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                                    <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;Value%
                                            </td>
                                            <td style="width: 224px; height: 21px" align="left">
                                                <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 21px" align="left" width="250">
                                                <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                    OnClick="btnFilter_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Panel ID="Panel2" runat="server" Width="99.8%" Height="199px" ScrollBars="Vertical" BorderWidth="1px">
                                    <asp:GridView ID="Grid_users" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="black" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                        Width="100%">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                        <RowStyle ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChbCustomer" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Distributor_id" HeaderText="Distributor_id">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Distributor_Name" HeaderText="Location">
                                                <ItemStyle BorderColor="Silver" Width="80px"  BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:Button ID="BtnViewPdf" runat="server" CssClass="Button" OnClick="BtnViewPdf_Click"
                            Text="View PDF" Width="90" />
                        <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                            Width="90" OnClick="btnViewExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
