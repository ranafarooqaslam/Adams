<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAssetNoMarking.aspx.cs" Inherits="Forms_frmAssetNoMarking" Title="SAMS :: Asset No Marking" %>

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
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--<tr>
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
                                        </tr>--%>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDescription" runat="server" Width="80px" Text="Remarks"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="3">
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                            <td style="width: 200px; height: 26px">
                                                <asp:CheckBox ID="chkWithAssetNo" runat="server" Width="97px" Text="With Asset No" AutoPostBack="True"
                                                    Checked="false" OnCheckedChanged="WithAssetCheck_Changed"></asp:CheckBox>
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
                                                <asp:Label ID="lblQty" runat="server" Width="85px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Asset No" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="width:70px;"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="8">
                                                <asp:Panel ID="Panel2" runat="server" Width="750px" Height="250px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GridAssetType" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GridAssetType_RowDeleting" OnRowCommand="GridAssetType_RowCommand"
                                                        ShowHeader="False" Width="725px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Asset_Marking_ID" ReadOnly="true" HeaderText="Asset_Marking_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
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

                                                            <asp:TemplateField HeaderText="Company Asset No">
                                                                <ItemStyle Width="85px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCompanyAssetNo" Width="85px" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                                    Width="35px" />
                                                            </asp:TemplateField>--%>
                                                           <%-- <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                            </asp:TemplateField>--%>
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
