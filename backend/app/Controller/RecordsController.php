<?php
App::uses('AppController', 'Controller');

class RecordsController extends AppController{

	var $uses = array('Record');
	
	function index(){
		$this->set('records',$this->Record->find('all'));
	}
}
?>