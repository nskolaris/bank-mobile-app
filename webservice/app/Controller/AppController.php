<?php
App::uses('Controller', 'Controller');

class AppController extends Controller {

	public $uses = array('Record','Tablet');
	public $helpers = array();

	var $tabletID = null;
	
	function beforeFilter(){
		parent::beforeFilter();
		if(!$this->authenticate_token()){
			exit;
		}
	}
	
	function authenticate_token(){
		$headers = apache_request_headers();
		if(isset($headers['Authorization'])){
			$token = str_replace('Bearer test_token','',$headers['Authorization']);
			if($this->Tablet->getByCode($token)){
				$this->tabletID = $token;
				return true;
			}else{
				$this->Record->addRecord('Intento de login (device_id)',$token);
			}
		}
		return false;
	}
}