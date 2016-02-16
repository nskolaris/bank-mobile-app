<?php
App::uses('AppController', 'Controller');

class TabletsController extends AppController{

	var $uses = array('Tablet');
	
	function beforeFilter(){
		parent::beforeFilter();
	}
	
	function index(){
		$this->set('tablets',$this->Tablet->find('all'));
	}
	
	function agregar($code = null){
		if(!empty($this->data)){
			$response = $this->Tablet->agregar($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'tablets','action'=>'index'));
			}else{
				$this->set('errores',$this->Tablet->invalidFields);
			}
		}
		$this->set('code',$code);
		$this->view = 'agregar';
	}
	
	function borrar($id){
		$response = $this->Tablet->borrar($id);
		$this->redirect(array('controller'=>'tablets','action'=>'index'));
	}
}
?>