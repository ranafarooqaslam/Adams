<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="SAMS :: SKU Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";
        }
        function pageLoad() {

            $("select").searchable();
        }
     
        function ClearSelection(lb) {
            lb.selectedIndex = -1;
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
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                   SKU Principal</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskuPrincipal" runat="server" Width="200px" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskuPrincipal_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td style="width: 110px" align="left">
                                                <strong>
                                                    Stock In Hand</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpStockInHand" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                   SKU Division</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskudivision" runat="server" Width="200px" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskudivision_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td >
                                                <strong>
                                                    Consumption ID</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpConsumption" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                    SKU Category</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskucategory" runat="server" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td >
                                                <strong>
                                                    Discount Allowed</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpDiscountAllowed" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                    SKU Brand</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddskuBrand" runat="server" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" Width="200px" AutoPostBack="True"
                                                    CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td >
                                                <strong>
                                                    Discount Recieved</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpDiscountRecieved" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                   GST On</strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpSKUTaxType" runat="server" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="T">Trade Price</asp:ListItem>
                                                    <asp:ListItem Value="R">Retail Price</asp:ListItem>
                                                    <asp:ListItem Value="E">Exempted</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td >
                                                <strong>
                                                   Scheme</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpScheme" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                   SKU Code</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtskucode" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                   Sale ID</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpSaleID" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <strong>
                                                   SKU Name</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtskuname" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td width="40px">
                                            </td>
                                            <td >
                                                <strong>
                                                   S.Tax (Sales)</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpSalesTax" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    Pack Size</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtpacksize" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                             <td >
                                                <strong>
                                                   S.Tax (Purchase)</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpSalesTaxPurchase" runat="server" Width="375" onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                   Unit In Case</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtunitincase" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td >
                                                <strong>
                                                   Is Active</strong>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbActive" runat="server" Checked="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                             <td>
                                                <strong>
                                                  Bar Code</strong>
                                            </td>
                                            <td style="width: 37px">
                                                <asp:TextBox ID="txtBarCode" runat="server" Width="192px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td >
                                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtunitincase"
                                                    FilterType="Numbers">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px">
                                            </td>
                                            <td style="height: 12px" align="left" colspan="2">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="86px" Font-Size="8pt"
                                                    Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 37px; height: 12px" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                </asp:ImageButton>&nbsp; Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table class="tblhead" width="980px">
                        <tbody>
                            <tr>
                                <td style="color: White;width: 155px; font-weight: bold;">
                                    <asp:Label ID="Label10" runat="server" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 22px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                        <asp:ListItem Value="Principal">Principal</asp:ListItem>
                                        <asp:ListItem Value="Division">Division</asp:ListItem>
                                        <asp:ListItem Value="Category">Category</asp:ListItem>
                                        <asp:ListItem>Brand</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 205px; height: 22px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="width: 350px; height: 22px" align="left">
                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Text="Filter"
                                        OnClick="btnFilter_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                        Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="grdSKUData" runat="server" Width="99.9%" 
                            OnPageIndexChanging="grdSKUData_PageIndexChanging" 
                            BackColor="White" AutoGenerateColumns="False" OnRowCommand="grdSKUData_RowCommand"
                            OnRowDeleting="grdSKUData_RowDeleting">
                           
                            <Columns>
                                <asp:BoundField DataField="Principal_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Division_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Category_Id" HeaderText="Category_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand_Id" HeaderText="Brand_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Principal" HeaderText="Principal">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Division" HeaderText="Division">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Category">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand" HeaderText="Brand">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PACKSIZE" HeaderText="Pack Size">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UNITS_IN_CASE" HeaderText="UIC">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STOCKINHAND_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONSUMPTION_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISCOUNTALLOW_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISCOUNTRECIEVED_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SCHEME_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="SALE_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SALESTAX_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SALESTAX_PURCHASE_ID">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ISACTIVE" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="BARCODE">
                                    <ItemStyle CssClass="HidePanel" />
                                    <HeaderStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        Width="35px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
