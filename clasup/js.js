var bEdit = 1;
var iCell = 0;

function deleteRow(){
	var objTab = $("#tabList").get(0);
	if(objTab!=null){
		for(var i= objTab.rows.length-1; i > 0; i--){
			if(objTab.rows[i].cells[0].firstChild.checked){
				objTab.deleteRow(i);
			}
		}
	}
}

function chkValue(objChk,setValue){
	var mValue = $("#idlist").val();
	mValue = mValue.replace(setValue + ",","");
	if(objChk.checked)
		mValue = mValue + setValue +",";
	$("#idlist").val(mValue);
}

function GetMultiple(obj1,obj2){
	var RcpPiece1 = obj1.replace(',','');
	var RcpPiece2 = obj2.replace(',','');
	if(RcpPiece1 == "") RcpPiece1 = "0";
	if(RcpPiece2 == "") RcpPiece2 = "0";
	var fltRcpPieceTotal = 0;
	
	if ( !isNaN(RcpPiece1) &&  !isNaN(RcpPiece2)) {
		fltRcpPieceTotal = parseFloat(RcpPiece1) * parseFloat(RcpPiece2);
	}
	else{
		return '0';
	}
	return fltRcpPieceTotal.toString();
}

function ChgList(i){
	var objtable = $("#tabList").get(0);
	tBox = objtable.rows[i].cells[6].firstChild.value;
	tPcs = objtable.rows[i].cells[7].firstChild.value;
	tQty = accMul(tBox,tPcs);
	objtable.rows[i].cells[8].firstChild.value=tQty;
	tPlanQty = objtable.rows[i].cells[9].firstChild.value;
	objtable.rows[i].cells[10].firstChild.value = accMinus(tQty,tPlanQty);
}

function ChgList1(i){
	var objtable = $("#tabList").get(0);
	tQty = objtable.rows[i].cells[8].firstChild.value;
	tPlanQty = objtable.rows[i].cells[9].firstChild.value;
	objtable.rows[i].cells[10].firstChild.value = accMinus(tQty,tPlanQty);
}

function format(item) {
	return item.inum
}
function LoadCombox(j){
	if(bEdit==1){
		$(function() {
				$("#tItem"+j).autocomplete("../ajax.aspx?opr=items",{
				multiple: false,width:200,
				dataType: "json",
				parse: function(data) {
					return $.map(data, function(row) {
						return {
							data: row,
							value: row.iname,
							result: row.iname
						}
					});
				},
				formatItem: function(row, i, max) {return "[" + row.inum + "]" + row.iname;},
				formatMatch: function(row, i, max) {return row.inum + " " + row.iname;}
				}).result(function(e, item) {
					$("#tPcs"+j).val(format(item));ChgList(j);
				});
				
				$("#tType"+j).autocomplete("../ajax.aspx?opr=wh",{
				multiple: false,
				dataType: "json",
				parse: function(data) {
					return $.map(data, function(row) {
						return {
							data: row,
							value: row.code,
							result: row.code
						}
					});
				},
				formatItem: function(row, i, max) {return "[" + row.id + "]" + row.code;},
				formatMatch: function(row, i, max) {return "[" + row.id + "]" + row.code;}
				}).result(function(e, item) {
					$("#tUnit"+j).val(item.id);
				});
		});
	}
}

function AddRow(){
    AddList("-1", "", "", "", 0, 0, "", 1, 0, "", "", "")
}
function AddList(tID, tItem, tUnit, tType, tPlanQty, tQty, tMemo, tBox, tPcs, tPallet, tGoods, tPackage){
	var strflag,strRead;
	var objTab = $("#tabList").get(0);
	if(objTab==null) return false;
	iCell = iCell+1;
	if(bEdit!=1)
	{strflag = " disabled = true";strRead = " readonly=true ";}
	if(tBox<=1 && parseInt(tPcs)>0) tBox = parseInt(tQty/tPcs);
	
	var objNewRow = objTab.insertRow();
	var objNewCell = objNewRow.insertCell();
	objNewCell.innerHTML = "<input type=\"checkbox\" onclick='chkValue(this,"+tID+");' id=\"seline\" name=\"seline\" "+ strflag +"><input type=\"hidden\" id=\"se_line\" name=\"se_line\" value=\"" + tID + "\">";
	
	objNewCell = objNewRow.insertCell();
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tItem"+iCell+"\" name=\"tItem\" value=\"" + tItem + "\"  style=\"width:90px;\">";

	objNewCell = objNewRow.insertCell();
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tPackage"+iCell+"\" name=\"tPackage\" value=\"" + tPackage + "\"  style=\"width:90px;\">";

	objNewCell = objNewRow.insertCell();
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tPallet"+iCell+"\" name=\"tPallet\" value=\"" + tPallet + "\"  style=\"width:90px;\">";

	objNewCell = objNewRow.insertCell();
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tGoods"+iCell+"\" name=\"tGoods\" value=\"" + tGoods + "\"  style=\"width:90px;\">";

	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"hidden\" id=\"tUnit"+iCell+"\" name=\"tUnit\" value=\"" + tUnit + "\"><input type=\"text\""+ strRead +" id=\"tType"+iCell+"\" name=\"tType\" value=\"" + tType + "\" style=\"width:40px;\">";

	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tBox"+iCell+"\" name=\"tBox\" onchange='ChgList("+ iCell +");' value=\"" + tBox + "\" style=\"width:30px;\">";

	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tPcs"+iCell+"\" name=\"tPcs\" onchange='ChgList("+ iCell +");' value=\"" + tPcs + "\" style=\"width:50px;\">";
	
	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tQty"+iCell+"\" name=\"tQty\" onchange='ChgList("+ iCell +");' value=\"" + tQty + "\" style=\"width:70px;\">";
	
	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tPlanQty"+iCell+"\" name=\"tPlanQty\" onchange='ChgList1("+ iCell +");' value=\"" + parseInt(tPlanQty-0) + "\" style=\"width:70px;\">";

	objNewCell = objNewRow.insertCell();
	if(parseInt(tQty-0)>0 && parseInt(tPlanQty-0)>0)
	objNewCell.innerHTML =  "<input type=\"text\" readonly=true id=\"tNum"+iCell+"\" value=\"" + (tQty-tPlanQty) + "\" name=\"tNum\" style=\"width:50px;\">";
	else
	objNewCell.innerHTML =  "<input type=\"text\" readonly=true id=\"tNum"+iCell+"\" value='0' name=\"tNum\" style=\"width:50px;\">";
	
	objNewCell = objNewRow.insertCell(); 
	objNewCell.innerHTML =  "<input type=\"text\""+ strRead +" id=\"tMemo"+iCell+"\" name=\"ttMemo\" value=\"" + tMemo + "\" style=\"width:50px;\">";
	
	LoadCombox(iCell);
}
