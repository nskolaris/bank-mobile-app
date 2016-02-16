<?php
class Record extends AppModel {
	var $name = 'Record';
	
	/* Funciones */
	
	function addRecord($name,$value){
		$this->create();
		return $this->save(array('name'=>$name,'value'=>$value));
	}
}
?>