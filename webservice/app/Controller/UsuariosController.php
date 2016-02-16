<?php
App::uses('AppController', 'Controller');

class UsuariosController extends AppController{

	var $uses = array('Usuario');
	
	function sincronizar(){
		$post = json_decode($_POST['data']);
		if(!empty($post)){
			foreach($post as $usuario){
				$usuario = (array)$usuario;
				if(!$this->Usuario->getByUsername($usuario['username'])){
					$this->Usuario->agregarPromotora($usuario);
				}
			}
		}
		echo json_encode($this->Usuario->getAll());
		exit;
	}
}
?>