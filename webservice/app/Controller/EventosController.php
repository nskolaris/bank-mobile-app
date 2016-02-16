<?php
App::uses('AppController', 'Controller');

class EventosController extends AppController{

	var $uses = array('Evento');

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