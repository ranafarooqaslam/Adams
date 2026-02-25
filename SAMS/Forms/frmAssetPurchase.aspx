<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAssetPurchase.aspx.cs" Inherits="Forms_frmAssetPurchase" Title="SAMS :: Add Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script language="JavaScript" type="text/javascript">

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

    <style type="text/css">
   .ajax__calendar_container { z-index : 1000 ; }
</style>
    <div id="right_data">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                         <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="94px" Text="Supplier" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="ddlSupplier" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="80px" Height="13px" Text="Purchase Date"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left"><asp:TextBox ID="txtPurDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                            </td>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtPurDate"
                                                PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy" OnClientShown="">
                                            </cc1:CalendarExtender>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDescription" runat="server" Width="80px" Text="Remarks"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="4">
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                 <asp:Label ID="lblAsset" runat="server" Width="250px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text=" Asset" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSerial1" runat="server" Width="75px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Serial #.1" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSerial2" runat="server" Width="75px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Serial #.2" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblRate" runat="server" Width="70px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Rate" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblColor" runat="server" Width="70px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Color" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblYear" runat="server" Width="70px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Mfg. Year" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblQty" runat="server" Width="70px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Qty" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add Asset" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlAsset_SelectedIndexChanged"
                                                     runat="server" Width="248px" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSerial1" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSerial2" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRate" runat="server" Width="65px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtColor" runat="server" Width="65px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtYear" runat="server" Width="65px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" runat="server" Width="65px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="8">
                                                <asp:Panel ID="Panel2" runat="server" Width="840px" Height="200px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GridAssetType" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GridAssetType_RowDeleting" OnRowCommand="GridAssetType_RowCommand"
                                                        ShowHeader="False" Width="820px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Asset_PurchaseDetail_ID" ReadOnly="true" HeaderText="Asset_PurchaseDetail_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AssetTypeID" ReadOnly="true" HeaderText="AssetTypeID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="AssetTypeName" ReadOnly="true" HeaderText="Asset">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SerialNo1" ReadOnly="true" HeaderText="Serial #.1">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SerialNo2" ReadOnly="true" HeaderText="Serial #.2">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Rate" ReadOnly="true" HeaderText="Rate">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Color" ReadOnly="true" HeaderText="Color">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="70px" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="MfgYear" ReadOnly="true" HeaderText="Mfg. Year">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Qty" ReadOnly="true" HeaderText="Qty">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Purchase_Date" ReadOnly="true" HeaderText="Purchase Date">
                                                              <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IsSerialNoBased" ReadOnly="true" HeaderText="IsSerialNoBased">
                                                              <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                                    Width="35px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document"  OnClick="btnSaveDocument_Click" CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                <strong>
                                    <asp:Label ID="Label4" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="88px"
                                    CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDocument" /> 
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
