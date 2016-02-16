<?php
class Usuario extends AppModel {
	var $name = 'Usuario';
	
	public $validate = array(
		'username' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar un nombre de usuario.'
		),
		'password' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar una contraseña.'
		),
		'password_tablet' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar una contraseña para la tablet.'
		),
		'nombre' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar un nombre.'
		),
		'apellido' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar un apellido.'
		)
    );
	
	/* Funciones */
	
	function getById($id){
		return $this->find('first',array('conditions'=>array('Usuario.id'=>$id)));
	}
	
	function getByUsername($username){
		return $this->find('first',array('conditions'=>array('Usuario.username'=>$username)));
	}
	
	function getAll(){
		return $this->find('all',array('fields'=>array('id','username','password','password_tablet','nombre','apellido','admin','deleted')));
	}
	
	function beforeSave($options = array()){
		if(!empty($this->data['Usuario']['password'])){
			$this->data['Usuario']['password'] = md5($this->data['Usuario']['password']);
		}
		return true;
	}
	
	function afterFind($results, $primary = false){
		foreach($results as $key => $val){
			if(isset($val['Usuario'])){
				if($val['Usuario']['deleted'] != null){
					unset($results[$key]);
				}
			}
		}
		return $results;
	}
	
	function agregarPromotora($data){
		$response = array('status'=>false,'message'=>'No se pudo agregar la promotora');
		$data['admin'] = 0;
		$this->create();
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se agregó la promotora con éxito';
		}
		return $response;
	}
	
	function agregarAdmin($data){
		$response = array('status'=>false,'message'=>'No se pudo agregar el administrador');
		$data['Usuario']['admin'] = 1;
		$this->create();
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se agregó el administrador con éxito';
		}
		return $response;
	}
	
	function modificar($data){
		$response = array('status'=>false,'message'=>'No se pudo modificar el usuario');
		if($data['Usuario']['password'] == ""){unset($data['Usuario']['password']);}
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se modificó el usuario con éxito';
		}
		return $response;
	}
	
	function borrar($id){
		$this->id = $id;
		return $this->saveField('deleted',date('Y-m-d h:i:s'));
	}
	
	function login($username, $password){
		return $this->find('first',array('conditions'=>array('Usuario.username'=>$username,'Usuario.password'=>md5($password),'Usuario.admin'=>1)));
	}
}
?>