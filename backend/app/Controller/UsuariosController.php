<?php
App::uses('AppController', 'Controller');

class UsuariosController extends AppController{

	var $uses = array('Usuario');
	
	function beforeFilter(){
		parent::beforeFilter();
	}
	
	function login(){
		$this->logout();
		$this->layout = 'login-theme';
		if(!empty($this->data['Usuario']['username']) && !empty($this->data['Usuario']['password'])){
			if($user = $this->Usuario->login($this->data['Usuario']['username'],$this->data['Usuario']['password'])){
				$this->Usuario->clearLoginAttempts($user['Usuario']['id']);
				$this->registerLogin($user);
				$this->redirect(array('controller'=>'usuarios','action'=>'index'));
			}else{
				if($user = $this->Usuario->getByUsername($this->data['Usuario']['username'])){
					if($user['Usuario']['banned'] == 0){
						$attempts = $this->Usuario->addLoginAttempt($user);
						$this->set('alerta','Contraseña incorrecta, luego de '.(4-$attempts).' intentos el usuario será bloqueado');
						if($attempts > 3){
							$this->set('alerta','El usuario ha sido bloqueado, contáctese con el administrador');
							$this->Usuario->ban($user['Usuario']['id'],1);
						}
					}else{
						$this->set('alerta','El usuario está bloqueado, contáctese con el administrador');
					}
				}
			}
		}
	}
	
	function index(){
		$this->set('usuarios',$this->Usuario->find('all'));
	}
	
	function agregarAdministrador(){
		if(!empty($this->data)){
			$response = $this->Usuario->agregarAdmin($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'usuarios','action'=>'index'));
			}else{
				$this->set('errores',$this->Usuario->invalidFields);
			}
		}
		$this->view = 'agregar';
	}
	
	function agregarPromotora(){
		if(!empty($this->data)){
			$response = $this->Usuario->agregarPromotora($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'usuarios','action'=>'index'));
			}else{
				$this->set('errores',$this->Usuario->invalidFields);
			}
		}
		$this->view = 'agregar';
	}
	
	function modificar($id){
		if(!empty($this->data)){
			$response = $this->Usuario->modificar($this->data);
			if($response['status']){
				$this->redirect(array('controller'=>'usuarios','action'=>'index'));
			}else{
				$this->set('errores',$this->Usuario->invalidFields);
			}
		}
		$this->set('usuario',$this->Usuario->getById($id));
		$this->view = 'agregar';
	}
	
	function borrar($id){
		$response = $this->Usuario->borrar($id);
		$this->redirect(array('controller'=>'usuarios','action'=>'index'));
	}
	
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
	
	function toggle_ban($usuario_id){
		if($usuario = $this->Usuario->getById($usuario_id)){
			if($usuario['Usuario']['banned'] == 1){
				$this->Usuario->ban($usuario['Usuario']['id'],0);
			}else{
				$this->Usuario->ban($usuario['Usuario']['id'],1);
			}
		}
		$this->redirect(array('controller'=>'usuarios','action'=>'index'));
	}
}
?>