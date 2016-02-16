<?php
App::uses('AppController', 'Controller');

class EventosController extends AppController{

	var $uses = array('Evento');
	
	function index(){
		$this->set('eventos',$this->Evento->find('all'));
	}
	
	function agregar(){
		if(!empty($this->data)){
			$response = $this->Evento->agregar($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'eventos','action'=>'index'));
			}else{
				$this->set('errores',$this->Evento->invalidFields);
			}
		}
	}
	
	function modificar($id){
		if(!empty($this->data)){
			$response = $this->Evento->modificar($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'eventos','action'=>'index'));
			}else{
				$this->set('errores',$this->Evento->invalidFields);
			}
		}
		$this->set('evento',$this->Evento->getById($id));
		$this->view = 'agregar';
	}
	
	function borrar($id){
		$response = $this->Evento->borrar($id);
		$this->redirect(array('controller'=>'eventos','action'=>'index'));
	}

	function sincronizar(){
		$post = json_decode($_POST['data']);
		if(!empty($post)){
			$nuevos_eventos = array();
			foreach($post as $evento){
				$evento = (array)$evento;
				if(!$this->Evento->getByCode($evento['code'])){
					if($this->Evento->agregar($evento)){
						$nuevos_eventos[] = $evento['code'];
					}
				}
			}
		}
		echo json_encode(array('eventos'=>$this->Evento->getAll(),'nuevos_eventos'=>$nuevos_eventos));
		exit;
	}
}
?>