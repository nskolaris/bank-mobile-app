<?php
App::uses('Controller', 'Controller');

class AppController extends Controller {

	public $uses = array('Record','Tablet');
	public $helpers = array();

	var $loggedUser = null;
	
	function beforeRender(){
		if($this->name == 'CakeError'){
			$this->layout = 'error';
		}        
	}
	
	function beforeFilter(){
		parent::beforeFilter();
		$this->layout = 'default-theme';
		$allowed_actions = array('login','sincronizar');
		if(!in_array($this->request->params['action'],$allowed_actions)){
			if(!$this->authenticate_admin()){
				$this->redirect(array('controller'=>'usuarios','action'=>'login'));
			}else{
				$this->set('loggedUser',$this->loggedUser);
			}
		}else{
			$token_actions = array('sincronizar');
			if(in_array($this->request->params['action'],$token_actions)){
				if(!$this->authenticate_token()){
					exit;
				}
			}
		}
	}
	
	function logout(){
		$this->Session->write('loggedUser',null);
	}
	
	function registerLogin($user){
		$this->Session->write('loggedUser',$user['Usuario']);
		$this->loggedUser = $user['Usuario'];
	}
	
	function authenticate_admin(){
		$this->loggedUser = $this->Session->read('loggedUser');
		if(empty($this->loggedUser)){
			return false;
		}
		return true;
	}
	
	function authenticate_token(){
		$headers = apache_request_headers();
		if(isset($headers['Authorization'])){
			$token = str_replace('Bearer test_token','',$headers['Authorization']);
			if($this->Tablet->getByCode($token)){
				return true;
			}else{
				$this->Record->addRecord('Intento de login (device_id)',$token);
			}
		}
		return false;
	}
}